using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils {
    public class Spawner : MonoBehaviour {
        public static Spawner instance { get; private set; }

        [field: SerializeField] public int numOfSpawns { set; private get; } = 3;
        [SerializeField] private GameObject spawnObject;
        [SerializeField] private float bufferDistance = 1.2f;

        private int storedSpawns;
        private Vector2 position;
        private List<Vector2> usedPosition = new List<Vector2>();

        // private void Start() {
        //     storedSpawns = numOfSpawns;
        // }

        // private void Update() {
        //     if (Input.GetKeyDown(KeyCode.L)) {
        //         Spawn(6);
        //     }
        // }

        public void Spawn(int _numOfSpawns) {
            while (_numOfSpawns > 0) {
                bool validPosition = false;
                
                while (!validPosition) {
                    position = new Vector2(Random.Range(-13, 13), 0);
                    validPosition = true;

                    foreach (Vector2 usedPos in usedPosition) {
                        float distance = Vector2.Distance(position, usedPos);
                        
                        if (Vector2.Distance(position, usedPos) < bufferDistance) {
                            validPosition = false;
                            // Debug.Log($"Position {position} is too close to {usedPos} (Distance: {distance})");
                            break;
                        }
                    }
                    // This will cause overlapping...would need to add a buffer to prevent
                    /*while (usedPosition.Contains(position)) {
                        position = new Vector2(Random.Range(-13, 13), 0);
                    }*/
                }
                usedPosition.Add(position);
                Instantiate(spawnObject, position, Quaternion.identity);
                _numOfSpawns--;
            }
            usedPosition.Clear();
        }
    }
}
