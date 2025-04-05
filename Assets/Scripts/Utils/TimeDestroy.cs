using UnityEngine;

namespace Utils {
	public class TimeDestroy : MonoBehaviour {

		public float timeDestroy = 1;
		// Use this for initialization
		void Start () {
			Destroy (gameObject, timeDestroy);
		}
	}
}