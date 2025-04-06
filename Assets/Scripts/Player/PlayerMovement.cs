using UnityEngine;

namespace Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour {
        public float moveSpeed = 5f; // Movement speed
        [SerializeField] private Vector2 minBound = new Vector2(-10f, -10f); // Minimum bounds
        [SerializeField] private Vector2 maxBound = new Vector2(10f, 10f); // Maximum bounds

        [Header("Graphics")] 
        [SerializeField] private Sprite gauche;
        [SerializeField] private Sprite centre;
        [SerializeField] private Sprite droit;

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        void Start() {
            rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update() {
            MovePlayer();
            UpdateSprite();
        }
        
        void MovePlayer() {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            // Calculate movement direction
            Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

            // Apply movement
            rb.linearVelocity = moveDirection * moveSpeed;

            // Apply movement bounds
            Vector2 clampedPosition = rb.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBound.x, maxBound.x);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBound.y, maxBound.y);

            // Set the clamped position back to Rigidbody2D
            rb.position = clampedPosition;
        }

        void UpdateSprite() {
            float moveX = Input.GetAxisRaw("Horizontal");

            if (moveX < 0 && Player.instance.takeDamage) {
                spriteRenderer.sprite = gauche;
            } else if (moveX > 0 && Player.instance.takeDamage) {
                spriteRenderer.sprite = droit;
            } else if (Player.instance.takeDamage) {
                spriteRenderer.sprite = centre;
            }
        }
    }
}
