using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
	public class PoolController : MonoBehaviour {

		public static PoolController instance;

		[System.Serializable]
		public class Pool {
			public string tag;
			public GameObject prefab;
			public int size;
		}
	
		public List<Pool> pools;
		private Dictionary<string, Queue<GameObject>> poolDictionary;
		private Dictionary<GameObject, string> prefabToTag;

		private void Awake() {
			instance = this;
		}

		private void Start() {
			InitializePool();
		}

		private void InitializePool() {
			poolDictionary = new Dictionary<string, Queue<GameObject>>();
			prefabToTag = new Dictionary<GameObject, string>();

			foreach (Pool pool in pools) {
				Queue<GameObject> objectPool = new Queue<GameObject>();

				for (int i = 0; i < pool.size; i++) {
					GameObject obj = Instantiate(pool.prefab);
					obj.SetActive(false);
					objectPool.Enqueue(obj);
				}

				poolDictionary.Add(pool.tag, objectPool);
				prefabToTag.Add(pool.prefab, pool.tag);
			}
		}

		public GameObject SpawnFromPool(string tag, Vector2 position, Quaternion rotation) {

			if (!poolDictionary.ContainsKey(tag)) {
				Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
				return null;
			}

			GameObject objectToSpawn = poolDictionary[tag].Dequeue();

			objectToSpawn.SetActive(true);
			objectToSpawn.transform.position = position;
			objectToSpawn.transform.rotation = rotation;

			poolDictionary[tag].Enqueue(objectToSpawn);

			return objectToSpawn;
		}

		public string GetTagFromPrefab(GameObject prefab) {
			if (prefabToTag.ContainsKey(prefab)) {
				return prefabToTag[prefab];
			}
			Debug.Log("Prefab not found in the pool map");
			return null;
		}
	}
}