using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Controllers {
    public class LevelController : MonoBehaviour {
        public static LevelController instance;
        [Header("Game Objects")]
        [SerializeField] private GameObject player;

        [Header("Wave Settings")]
        [SerializeField] private Vector2 spawnPosition;
        [SerializeField] private int totalWaves = 1;
        [SerializeField] private float waveWait = 5f;
        [SerializeField] private int enemyCount;

        [Header("Time Settings")] 
        [SerializeField] private bool countdownTimer;
        public float spawnWait;
        [SerializeField] private float startWait;

        private PoolController poolController;
        private List<string> enemyTags = new List<string>();
        private int waveCount;

        [HideInInspector] public static bool playerDead;

        private void Awake() {
            Instantiate(player, new Vector2(0, 3), Quaternion.identity);
        }

        private void Start() {
            instance = this;
            poolController = PoolController.instance;

            // Grab all enemy tags from PoolController that start with "Enemy"
            foreach (var pool in poolController.pools) {
                if (pool.tag.StartsWith("Enemy")) {
                    enemyTags.Add(pool.tag);
                }
            }

            if (enemyTags.Count == 0) {
                Debug.LogError("No enemy tags found in PoolController!");
                return;
            }

            waveCount = 0;
            StartCoroutine(EnemyWaves());
        }

        private void Update() {
            if (Input.GetButtonDown("Cancel")) {
                SceneManager.LoadScene("Title");
            }
        }

        public static IEnumerator GameOver() {
            Player.Player.instance.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
        }

        private IEnumerator EnemyWaves() {
            yield return new WaitForSeconds(startWait);

            for (int wave = 0; wave < totalWaves; wave++) {
                for (int i = 0; i < enemyCount; i++) {
                    // Vector2 randomSpawnPos = new Vector2(spawnPosition.x, Random.Range(-spawnPosition.x, spawnPosition.x));
                    Vector2 randomSpawnPos = new Vector2(Random.Range(-5f, 5f), -6f);
                    string randomTag = enemyTags[Random.Range(0, enemyTags.Count)];

                    GameObject enemy = poolController.SpawnFromPool(randomTag, randomSpawnPos, Quaternion.identity);

                    if (enemy == null) {
                        Debug.LogWarning($"Could not spawn enemy with tag: {randomTag}");
                    }

                    yield return new WaitForSeconds(spawnWait);
                }
                
                totalWaves++;
                yield return new WaitForSeconds(waveWait);
            }

            Debug.Log("All waves completed.");
        }
    }
}
