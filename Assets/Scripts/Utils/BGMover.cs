using UnityEngine;

namespace Utils {
    public class BGMover : MonoBehaviour {
        public enum MovementType : byte {
            Standard,
            Reset
        }

        public MovementType movementType;
        [Header("Settings for Standard")]
        [SerializeField] private float movePercentage = 0.5f;
        [SerializeField] private float endCoord = -30;
        [Header("Settings for Reset")]
        [SerializeField] private Vector2 resetToo;
        [SerializeField] private bool matchPlayerSpeed;

        public bool moving;

        private System.Action movementAction;

        private void Start() {
            switch (movementType) {
                case MovementType.Standard:
                    movementAction = StandardMovement;
                    break;
                case MovementType.Reset:
                    movementAction = ResetMovement;
                    break;
                default:
                    Debug.LogError("Invald movement type");
                    break;
            }
        }

        private void FixedUpdate() {
            movementAction?.Invoke();
        }

        private void StandardMovement() {
            var pos = transform.position;
            pos.y += Player.Player.instance.playerMovement.moveSpeed * movePercentage * Time.deltaTime;

            if (pos.y > endCoord)
                pos.y = 0;
            
            transform.position = pos;
        }

        private void ResetMovement() {
            if (moving) {
                var pos = transform.position;
                if (!matchPlayerSpeed){
                    pos.x -= Player.Player.instance.playerMovement.moveSpeed * movePercentage * Time.deltaTime;
                } else if (matchPlayerSpeed) {
                    pos.x -= Player.Player.instance.playerMovement.moveSpeed * Time.deltaTime;
                }

                if (pos.x < endCoord) {
                    pos = resetToo;
                    // Temp solution will need to change if I want an object to reset and move through
                    // without having to have a trigger
                    moving = false;
                }
                
                transform.position = pos;
            }
        }

        /* [Header("Movement settings")]
        [SerializeField] private float movePercentage = 0.5f;
        [SerializeField] private float endCoord = -30;

        private void FixedUpdate() {
            var pos = transform.position;
            pos.x -= Player.Player.instance.playerMovement.moveSpeed * movePercentage * Time.deltaTime;

            if (pos.x < endCoord)
                pos.x = 0;
            
            transform.position = pos;
        } #1#*/
    }
}
