using Controllers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {
    public static Player instance { get; private set; }
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 5f;

    [Header("Jumping")] 
    // [SerializeField] private float jumpForce = 10f;
    // [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Dirt")]
    [SerializeField] private GameObject dirtPile;
    [SerializeField] private Transform dirtLocation;
    public bool canDig;

    [Header("Scale")]
    [SerializeField] private float maxSoulWeight = 100f; // adjust this to your design
    [SerializeField] private GameObject scale;
    [SerializeField] private GameObject scaleMarker;
    public bool scaleOn;

    public int soulWeight;
    public bool talkToWitch;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDigging;
    private Grave currentGrave;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject); // Or handle if multiple instances are detected
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update() {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded && !isDigging && canDig) {
            isDigging = true;
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            // ReSharper disable once Unity.UnknownAnimatorStateName
            anim.Play("dig");
            Invoke(nameof(EndDig), 1f);
            return;
        }

        if (Input.GetButtonDown("Jump") && isGrounded && talkToWitch) {
            Debug.Log("Talking to the Witch");
            HandInSouls();
        }

        if (isDigging) return;
        
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        // Flip character sprite based on direction
        if (moveInput != 0 && !isDigging) {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1f, 1f);
            anim.Play("walk");
        } else if (moveInput == 0 && isGrounded) {
            anim.Play("idle");
        }

        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log(LevelController.soulWeight);
        }
    }
    private void HandInSouls() {
        // Clamp soulWeight to avoid going out of bounds
        soulWeight = Mathf.Clamp(soulWeight, 0, (int)maxSoulWeight);

        // Normalize soulWeight to [0, 1]
        float weightPercentage = soulWeight / maxSoulWeight;
        Debug.Log(weightPercentage);

        // Map weightPercentage to [-4, 4]
        float xPos = Mathf.Lerp(-4f, 4f, weightPercentage);

        // Apply to position
        Vector3 pos = scaleMarker.transform.position;
        pos.x += xPos;
        scaleMarker.transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D _other) {
        if (_other.CompareTag("Grave")) {
            Grave grave = _other.GetComponent<Grave>();

            if (grave != null) {
                currentGrave = grave;
            }
        }
    }

    public void ScaleOn() {
        scaleOn = !scaleOn;
        scale.SetActive(scaleOn);
    }

    private void EndDig() {
        isDigging = false;
        Instantiate(dirtPile, dirtLocation.position, Quaternion.identity);
        if (currentGrave != null) {
            currentGrave.SpawnSoul();
        }
    }

    void OnDrawGizmosSelected() {
        if (groundCheck != null) {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
