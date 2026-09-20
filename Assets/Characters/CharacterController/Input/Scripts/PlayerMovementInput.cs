using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour, InputController.IMovementMapActions
{
    public InputController inputController;
    public Vector2 movementInput;

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        print(movementInput);
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
