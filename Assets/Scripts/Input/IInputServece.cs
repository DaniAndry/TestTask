using UnityEngine;

namespace Input
{
    public class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        private readonly SwipeInput _swipeInput;

        public InputService(SwipeInput swipeInput)
        {
            _swipeInput = swipeInput;
        }
        
        public Vector2 AxisMovement =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        
        public Vector2 AxisRotation => _swipeInput.CurrentRotation;

        public Vector2 TouchPoint => _swipeInput.TouchPosition;
        
        public bool Touched => _swipeInput.IsTouched; 

        public void UpdateInput()
        {
            _swipeInput.Update();
        }
    }
}