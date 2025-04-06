using UnityEngine;

namespace Utils {
    public class DontDestroy : MonoBehaviour {
        public static DontDestroy instance;
        
        private void Awake() {
            if (instance == null) {
                instance = this;
            } else {
                Destroy(gameObject);
            }
            
            DontDestroyOnLoad(gameObject);
        }
    }
}
