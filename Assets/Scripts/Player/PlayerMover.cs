using Input;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMover : MonoBehaviour
    {
        private IInputService _inputService;
        private CharacterController _characterController;

        public float moveSpeed = 5f;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            (_inputService as InputService)?.UpdateInput();

            Vector2 axisMovement = _inputService.AxisMovement;
            Vector3 moveDirection = transform.forward * axisMovement.y + transform.right * axisMovement.x;
            
            _characterController.Move(moveDirection * (moveSpeed * Time.deltaTime));
            
            Vector2 axisRotation = _inputService.AxisRotation;
            if (axisRotation != Vector2.zero)
            {
                transform.Rotate(0, axisRotation.x, 0);
            }
        }
    }
}