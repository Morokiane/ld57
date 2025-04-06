using Controllers;
using UnityEngine;

namespace Utils {
    public class DepthTracker : MonoBehaviour {

        private Vector2 pos;

        private void Start() {
            pos = transform.position;  // Initialize pos with the current position of the object
        }

        private void FixedUpdate() {
            if (!LevelController.playerDead) {
                MoveDown(); // Call without passing pos since it's now a class variable}
            }
        }
        
        private void MoveDown() {
            pos.y -= Player.Player.instance.playerMovement.moveSpeed * Time.fixedDeltaTime;
            transform.position = pos;  // Apply the updated position
            Player.Player.instance.currentDepth = transform.position.y;
        }
    }

}
