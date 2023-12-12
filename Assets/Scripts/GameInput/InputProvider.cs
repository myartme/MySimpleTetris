using UnityEngine.InputSystem;

namespace GameInput
{
    public abstract class InputProvider
    {
        public InputAction[] MovementInputAction { get; protected set; }
        public InputAction[] RotationInputAction { get; protected set; }

        public abstract MovementType GetMovementAction(InputAction.CallbackContext callbackContext);

        public abstract RotationType GetRotationAction(InputAction.CallbackContext callbackContext);
        
        public enum MovementType
        {
            Left,
            Right,
            Down
        }

        public enum RotationType
        {
            Anticlockwise,
            Clockwise
        }
    }
}