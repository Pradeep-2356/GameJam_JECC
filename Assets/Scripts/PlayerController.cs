using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 4f;
    public float rotationSpeed = 8f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private Animator animator;

    private bool isGrounded;
    private bool isPerformingAction;

    private Vector3 moveInput;
    private float targetSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckGround();

        if (!isPerformingAction)
        {
            ReadMovementInput();
            HandleActions();
        }

        UpdateAnimator();
    }

    void FixedUpdate()
    {
        if (!isPerformingAction)
            MoveCharacter();
        else
            rb.linearVelocity = Vector3.zero;
    }

    // ---------------- MOVEMENT ----------------

    void ReadMovementInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        moveInput = new Vector3(h, 0, v).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        targetSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveInput.magnitude > 0.1f)
        {
            Quaternion rot = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
    }

    void MoveCharacter()
    {
        Vector3 velocity = moveInput * targetSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
    }

    // ---------------- ACTIONS ----------------

    void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PerformAction("Roll");
        }

        if (Input.GetMouseButtonDown(0))
        {
            PerformAction("LightAttack");
        }

        if (Input.GetMouseButtonDown(1))
        {
            PerformAction("HeavyAttack");
        }
    }

    void PerformAction(string trigger)
    {
        if (isPerformingAction) return;

        isPerformingAction = true;
        animator.SetTrigger(trigger);
    }

    // ---------------- ANIMATION ----------------

    void UpdateAnimator()
    {
        float animSpeed = moveInput.magnitude * targetSpeed;
        animator.SetFloat("Speed", animSpeed);
        animator.SetBool("IsGrounded", isGrounded);
    }

    // ---------------- GROUND ----------------

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer
        );
    }

    // ---------------- ANIMATION EVENT ----------------

    // CALL THIS AT END OF Roll / Attack animations
    public void EndAction()
    {
        isPerformingAction = false;
    }
}
