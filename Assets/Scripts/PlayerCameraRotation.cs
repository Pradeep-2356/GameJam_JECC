using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMouseRotation : MonoBehaviour
{
    [Header("References")]
    public Transform cameraTransform;   // Main Camera
    public float rotationSpeed = 12f;

    [Header("Input")]
    public InputActionReference lookAction; // Look (Vector2)

    void Update()
    {
        RotatePlayerWithMouse();
    }

    void RotatePlayerWithMouse()
    {
        if (cameraTransform == null || lookAction == null)
            return;

        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();

        // Only rotate player when mouse moves horizontally
        if (Mathf.Abs(lookInput.x) < 0.01f)
            return;

        // Get camera forward direction (ignore vertical tilt)
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;

        if (camForward.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(camForward);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
