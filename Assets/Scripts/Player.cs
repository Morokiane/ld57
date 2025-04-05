using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jumping")] 
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Animator animator;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update() {
        // Horizontal movement
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Flip character sprite based on direction
        if (moveInput != 0) {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1f, 1f);
            // ReSharper disable once Unity.UnknownAnimatorStateName
            animator.Play("walk");
        } else if (moveInput == 0 && isGrounded) {
            // ReSharper disable once Unity.UnknownAnimatorStateName
            animator.Play("idle");
        }

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            // ReSharper disable once Unity.UnknownAnimatorStateName
            animator.Play("jump");
        }
    }

    private void FixedUpdate() {
        if (rb.linearVelocity.y < 0) {
            rb.linearVelocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected() {
        if (groundCheck != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
