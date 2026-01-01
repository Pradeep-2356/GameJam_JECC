using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float jumpForce = 6f;
    public float rotationSpeed = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    [Header("Interaction")]
    public float interactRange = 2f;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        CheckGround();
        HandleMovement();
        HandleJump();
        HandleInteraction();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 velocity = direction * currentSpeed;
        rb.linearVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);

        // Rotation
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Animation
        if (animator)
        {
            animator.SetFloat("Speed", direction.magnitude * currentSpeed);
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
        }
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundLayer
        );

        animator.SetBool("IsGrounded", isGrounded);
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Letter"))
                {
                    hit.GetComponent<LetterInteract>().Interact();
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);

        if (groundCheck)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}
