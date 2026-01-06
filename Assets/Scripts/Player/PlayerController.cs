using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float jumpForce = 4f;
    public float rotationSpeed = 10f;

    [Header("Camera")]
    public Transform cameraTransform;

    [Header("Ground Check")]
    public LayerMask groundLayers;

    [Header("Interaction")]
    public float interactRange = 2f;
    public LayerMask interactLayer;

    [Header("Audio")]
    public AudioSource walkingSound;
    public AudioSource runningSound;
    public AudioSource jumpSound;
    public AudioSource landSound;
    public AudioSource rollSound;
    public AudioSource lightAttackSound;
    public AudioSource heavyAttackSound;
    public AudioSource breathingSound;
    public AudioSource environmentSound;

    private Rigidbody rb;
    private Animator animator;

    private Vector3 moveInput;
    private float targetSpeed;
    private bool isGrounded;
    private bool wasGrounded;
    private bool isPerformingAction;
    private bool IsDead;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;

        // Breathing always ON
        breathingSound.loop = true;
        breathingSound.volume = 0.2f;
        breathingSound.Play();
        // Environment sound always ON
        environmentSound.loop = true;
        environmentSound.volume = 0.1f;
        environmentSound.Play();
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
        HandleMovementAudio();
        HandleLandingSound();
    }

    void FixedUpdate()
    {
        if (!isPerformingAction)
            MoveCharacter();
        else
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
    }

    // ---------------- MOVEMENT ----------------

    void ReadMovementInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        moveInput = (camForward.normalized * v + camRight.normalized * h).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        targetSpeed = isRunning ? runSpeed : walkSpeed;

        if (moveInput.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }

    void MoveCharacter()
    {
        rb.linearVelocity = new Vector3(moveInput.x * targetSpeed, rb.linearVelocity.y, moveInput.z * targetSpeed);
    }

    // ---------------- ACTIONS ----------------

    void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            jumpSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            PerformAction("Roll");
            rollSound.Play();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PerformAction("LightAttack");
            lightAttackSound.Play();
        }

        if (Input.GetMouseButtonDown(1))
        {
            PerformAction("HeavyAttack");
            heavyAttackSound.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    void TryInteract()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactRange, interactLayer);
        foreach (Collider hit in hits)
        {
            LetterInteract letter = hit.GetComponentInParent<LetterInteract>();
            if (letter != null)
            {
                letter.Interact();
                break;
            }
        }
    }

    void PerformAction(string trigger)
    {
        if (isPerformingAction) return;
        isPerformingAction = true;
        animator.SetTrigger(trigger);
    }

    // ---------------- AUDIO ----------------

    void HandleMovementAudio()
    {
        if (moveInput.magnitude > 0.1f && isGrounded && !isPerformingAction)
        {
            if (targetSpeed == runSpeed)
            {
                if (!runningSound.isPlaying) runningSound.Play();
                walkingSound.Stop();
                breathingSound.volume = 0.5f;
            }
            else
            {
                if (!walkingSound.isPlaying) walkingSound.Play();
                runningSound.Stop();
                breathingSound.volume = 0.3f;
            }
        }
        else
        {
            walkingSound.Stop();
            runningSound.Stop();
            breathingSound.volume = 0.2f;
        }
    }

    void HandleLandingSound()
    {
        if (!wasGrounded && isGrounded)
        {
            landSound.Play();
        }
        wasGrounded = isGrounded;
    }

    // ---------------- ANIMATION ----------------

    void UpdateAnimator()
    {
        animator.SetFloat("Speed", moveInput.magnitude * targetSpeed);
        animator.SetBool("IsGrounded", isGrounded);
    }

    // ---------------- GROUND ----------------

    void CheckGround()
    {
        isGrounded = Physics.Raycast(
            transform.position + Vector3.up * 0.1f,
            Vector3.down,
            0.4f,
            groundLayers
        );
    }

    // ---------------- ANIMATION EVENT ----------------

    public void EndAction()
    {
        if (IsDead) return;
        isPerformingAction = false;
    }
}
