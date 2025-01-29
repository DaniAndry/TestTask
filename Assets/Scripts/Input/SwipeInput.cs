using UnityEngine;

namespace Input
{
    public class SwipeInput
    {
        private Vector2 _startTouchPosition;
        private Quaternion _initialCameraRotation;
        private Vector2 _currentRotation;
        private bool _isSwipeDetected;

        private const float Sensitivity = 0.01f;
        private const float SwipeThreshold = 100f;

        public Vector2 CurrentRotation => _currentRotation;
        public Vector2 TouchPosition { get; private set; }
        public bool IsTouched { get; private set; }

        public void Update()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        _initialCameraRotation = Camera.main.transform.rotation;
                        _currentRotation = Vector2.zero;
                        _isSwipeDetected = false;
                        IsTouched = false; 
                        break;

                    case TouchPhase.Moved:
                        Vector2 delta = touch.position - _startTouchPosition;
                        if (delta.magnitude > SwipeThreshold)
                        {
                            _currentRotation = new Vector2(delta.x * Sensitivity, delta.y * Sensitivity);
                            _isSwipeDetected = true;
                        }
                        break;

                    case TouchPhase.Ended:
                        if (!_isSwipeDetected)
                        {
                            ProcessTouch(touch.position);
                        }
                        _currentRotation = Vector2.zero;
                        break;
                }
            }
            else
            {
                IsTouched = false;
            }
        }

        private void ProcessTouch(Vector2 screenPosition)
        {
            TouchPosition = screenPosition;
            IsTouched = true;
        }
    }
}
