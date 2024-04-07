using UnityEngine.InputSystem;

namespace GameInput
{
    public class GameplayInputProvider : InputProvider
    {
        private InputAction _movementLeft;
        private InputAction _movementRight;
        private InputAction _movementDown;
        public GameplayInputProvider(InputController inputController)
        {
            _movementLeft = inputController.Gameplay.MovementLeft;
            _movementRight = inputController.Gameplay.MovementRight;
            _movementDown = inputController.Gameplay.MovementDown;
            MovementInputAction = new[] { _movementLeft, _movementRight, _movementDown };
            RotationInputAction = new[] { inputController.Gameplay.Rotation };
        }
        
        public override MovementType GetMovementAction(InputAction.CallbackContext callbackContext)
        {
            return callbackContext.action == _movementLeft ? MovementType.Left : 
                callbackContext.action == _movementRight ? MovementType.Right :
                callbackContext.action == _movementDown ? MovementType.Down :
                throw new System.ArgumentException("GameGrid Action Movement is not support current data.");
        }

        public override RotationType GetRotationAction(InputAction.CallbackContext callbackContext)
        {
            var value = callbackContext.ReadValue<float>();
            
            return value < 0 ? RotationType.Anticlockwise :
                value > 0 ? RotationType.Clockwise : 
                throw new System.ArgumentException("GameGrid Action Rotation is not support current data.");
        }
    }
}