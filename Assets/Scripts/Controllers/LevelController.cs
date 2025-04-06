using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers {
    public class LevelController : MonoBehaviour {
        // Not being used...I don't think.
        // private delegate void PlayerAlive();
        // private static event PlayerAlive PlayerExists;
        // public static LevelController levelController;

        [Header("Game Objects")]
        [SerializeField] private GameObject[] enemies;
        [SerializeField] private GameObject player;
        [Header("Wave settings")]
        [SerializeField] private Vector2 spawnPosition;
        [SerializeField] private int totalWaves = 5;   // Total number of waves
        [SerializeField] private float waveWait = 5f;  // Time to wait between waves
        [SerializeField] private int enemyCount;
        [Header("Time settings")] 
        [SerializeField] private bool countdownTimer;
        [SerializeField] private float spawnWait; // Wait time for spawning to start
        [SerializeField] private float startWait; // ?
        [SerializeField] public float levelEndTimer; // When does the level end
        // private bool isPaused = false;
        private int waveCount;
        [HideInInspector] public static int numOfEnemiesKilled;
        [HideInInspector] public static bool playerDead;
    
        private PoolController poolController;

        private void Awake() {
            Instantiate(player, new Vector2(-10, 0), transform.rotation);
        }

        private void Start() {
            // levelController = this;
            poolController = PoolController.instance;

            waveCount = enemyCount + 1;
            waveCount = 0;
        
            StartCoroutine (EnemyWaves());
        }

        // private void FixedUpdate() {
        //     EnemyWaves();
        // }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator EnemyWaves() {
            yield return new WaitForSeconds(startWait); // Initial wait before the first wave starts

            for (int wave = 0; wave < totalWaves; wave++) {
                // Debug.Log("Starting Wave " + (wave + 1));
        
                // Spawn all enemies for the current wave
                for (int i = 0; i < enemyCount; i++) {
                    var spawnPosition = new Vector2(this.spawnPosition.x, Random.Range(-this.spawnPosition.x, this.spawnPosition.x));
                    // Spawn enemies from the pool instead of instantiating
                    poolController.SpawnFromPool("Enemy", spawnPosition, Quaternion.identity);

                    yield return new WaitForSeconds(spawnWait); // Wait between enemy spawns within a wave
                }

                yield return new WaitForSeconds(waveWait); // Wait between waves
            }

            Debug.Log("All waves completed.");
        }
    }
}
