using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool {
    public string tag;
    public GameObject prefab;
    public int size;
}

public class PoolController : MonoBehaviour {
    public static PoolController instance;

    [Header("Pool Settings")]
    public List<Pool> pools;

    private Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        InitializePool();
    }

    private void InitializePool() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++) {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
            Debug.Log($"[PoolController] Registered pool with tag: {pool.tag}");
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation) {
        if (!poolDictionary.ContainsKey(tag)) {
            Debug.LogError($"[PoolController] Pool with tag '{tag}' doesn't exist!");
            return null;
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
