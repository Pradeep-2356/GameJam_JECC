using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [Header("Follow Target")]
    [SerializeField] private Transform followTarget;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationalSpeed = 200f;
    [SerializeField] private float bottomClamp = -40f;
    [SerializeField] private float topClamp = 70f;

    private float cinemachineTargetPitch;
    private float cinemachineTargetYaw;

    private void LateUpdate()
    {
        CameraLogic();
    }

    private void CameraLogic()
    {
        float mouseX = GetMouseInput("Mouse X");
        float mouseY = GetMouseInput("Mouse Y");

        cinemachineTargetPitch = UpdateRotation(
            cinemachineTargetPitch,
            mouseY,
            bottomClamp,
            topClamp,
            true
        );

        cinemachineTargetYaw = UpdateRotation(
            cinemachineTargetYaw,
            mouseX,
            float.MinValue,
            float.MaxValue,
            false
        );

        ApplyRotations(cinemachineTargetPitch, cinemachineTargetYaw);
    }

    private void ApplyRotations(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private float GetMouseInput(string axis)
    {
        return Input.GetAxis(axis) * rotationalSpeed * Time.deltaTime;
    }
}
