using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementInput : MonoBehaviour, InputController.IMovementMapActions
{
    private InputController inputController;
    public Vector2 movementInput;
    public Vector2 lookInput;

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
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
}
