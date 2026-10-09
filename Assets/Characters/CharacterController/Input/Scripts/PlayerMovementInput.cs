using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour, InputController.IMovementMapActions
{
    private InputController inputController;
    public Vector2 movementInput;
    public Vector2 lookInput;
    public bool jumpPressed;
    public bool runPressed;
    public bool crouchPressed;

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        jumpPressed = true;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //if (!context.performed)
        //    return;
        if (context.performed)
            crouchPressed = true;

        if (context.canceled)
            crouchPressed = false;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        runPressed = true;
    }

    private void OnEnable()
    {
        inputController = new InputController();
        inputController.Enable();

        inputController.MovementMap.Enable();
        inputController.MovementMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        inputController.MovementMap.Disable();
        inputController.MovementMap.RemoveCallbacks(this);
    }

    private void LateUpdate()
    {
        jumpPressed = false;
        runPressed = false;
        //crouchPressed = false;
    }
}
