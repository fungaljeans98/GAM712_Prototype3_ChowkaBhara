using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{

    #region Initialisations
    InputSystem_Actions inputSystem;
    private InputAction rightButton;
    private InputAction leftButton;
    private InputAction jumpButton;

    [Header("Enable Movements")]
    [SerializeField] public bool enableRight;
    [SerializeField] public bool enableLeft;
    [SerializeField] public bool enableJump;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("GroundCheck")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake() {
        inputSystem = new InputSystem_Actions();
    }

    private void OnEnable() {
        rightButton = inputSystem.Player.MoveRight;
        rightButton.Enable();
        rightButton.performed += OnRight;

        leftButton = inputSystem.Player.MoveLeft;
        leftButton.Enable();
        leftButton.performed += OnLeft;

        jumpButton = inputSystem.Player.Jump;
        jumpButton.Enable();
        jumpButton.performed += OnTap;
    }
    private void OnDisable() {
        rightButton.performed -= OnRight;
        rightButton.Disable();

        leftButton.performed -= OnLeft;
        leftButton.Disable();

        jumpButton.performed -= OnTap;
        jumpButton.Disable();
    }
    #endregion

    #region Start, OnRight, OnLeft, OnTap
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnRight(InputAction.CallbackContext context) {
        if (enableRight)
        MoveRight();
    }
    void OnLeft(InputAction.CallbackContext context) {
        if (enableLeft)
        MoveLeft();
    }

    void OnTap(InputAction.CallbackContext context) {
        if (enableJump)
        Jump();
    }
    #endregion

    #region LOGIC
    void MoveRight() {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveSpeed;
        rb.linearVelocity = velocity;
    }
    void MoveLeft() {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = -moveSpeed;
        rb.linearVelocity = velocity;
    }

    void Jump() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius);

        if (isGrounded) {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    #endregion
    
}
