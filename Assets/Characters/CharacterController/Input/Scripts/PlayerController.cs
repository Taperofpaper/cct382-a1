using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera playerCamera;

    private PlayerMovementInput inputController;

    public float runAcceleration = 0.31f;
    public float runSpeed = 3.0f;
    public float drag = 0.12f;

    public float lookSensitivityH = 0.2f;
    public float lookSensitivityV = 0.2f;
    public float lookLimitV = 80f;

    private Vector2 cameraRotation = Vector2.zero;
    private Vector2 playerTargetRotation = Vector2.zero;

    private void Awake()
    {
        inputController = GetComponent<PlayerMovementInput>();
    }

    private void Update()
    {
        Vector3 cameraForwardXZ = new Vector3(playerCamera.transform.forward.x, 0f, playerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(playerCamera.transform.right.x, 0f, playerCamera.transform.right.z).normalized;

        Vector3 moveDirection = inputController.movementInput.x * cameraRightXZ + inputController.movementInput.y * cameraForwardXZ;

        Vector3 moveDelta = moveDirection * runAcceleration;
        Vector3 newVelocity = characterController.velocity + moveDelta;
        newVelocity = Vector3.ClampMagnitude(newVelocity, runSpeed);

        Vector3 currentDrag = newVelocity.normalized * drag * Time.deltaTime;
        if (newVelocity.magnitude > drag * Time.deltaTime)
        {
            newVelocity -= currentDrag;
        } else
        {
            newVelocity = Vector3.zero;
        }

        characterController.Move(newVelocity * Time.deltaTime);
    }
    private void LateUpdate()
    {
        cameraRotation.x += lookSensitivityH * inputController.lookInput.x;
        cameraRotation.y = Mathf.Clamp(cameraRotation.y - lookSensitivityV * inputController.lookInput.y, -lookLimitV, lookLimitV);

        playerTargetRotation.x += transform.eulerAngles.x + lookSensitivityH * inputController.lookInput.x;
        transform.rotation = Quaternion.Euler(0f, playerTargetRotation.x, 0f);

        playerCamera.transform.rotation = Quaternion.Euler(cameraRotation.y, cameraRotation.x, 0f);
    }
}
