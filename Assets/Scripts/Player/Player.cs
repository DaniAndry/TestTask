using Input;
using Items;
using UnityEngine;
using Zenject;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform takeTarget; 
        [SerializeField] private float throwForce = 5f; 
        [SerializeField] private LayerMask interactableLayer; 

        private Item _takedItem;
        private bool _isTakingItem;
        private Rigidbody _itemRigidbody;
        private IInputService _inputService;

        public float moveSpeed = 5f;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            if (_inputService.Touched)
            {
                TryInteract();
            }
        }
        
        private void TryInteract()
        {
            var ray = Camera.main.ScreenPointToRay(_inputService.TouchPoint);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
            {
                if (_isTakingItem && hit.collider.gameObject == _takedItem.gameObject)
                {
                    DropItem(); 
                }
                else
                {
                    var interactable = hit.collider.GetComponent<IItem>();

                    if (interactable != null) 
                    {
                        TakeItem(interactable);
                    }
                }
            }
        }
        
        private void TakeItem(IItem item)
        {
            if (_isTakingItem) return; 

            _takedItem = item as Item;
            if (_takedItem == null) return; 
            
            if (_takedItem.TryGetComponent(out _itemRigidbody))
            {
                _itemRigidbody.isKinematic = true; 
                _itemRigidbody.velocity = Vector3.zero;
                _itemRigidbody.angularVelocity = Vector3.zero;
                _itemRigidbody.interpolation = RigidbodyInterpolation.None;
            }
            
            _takedItem.transform.position = takeTarget.position;
            _takedItem.transform.rotation = takeTarget.rotation;
            _takedItem.transform.SetParent(takeTarget, true);

            _isTakingItem = true;
        }
        
        private void DropItem()
        {
            if (_takedItem == null) return;

            _takedItem.transform.SetParent(null); 

            if (_itemRigidbody != null)
            {
                _itemRigidbody.isKinematic = false; 
                _itemRigidbody.interpolation = RigidbodyInterpolation.Interpolate; 
                _itemRigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse); 
            }

            _takedItem = null;
            _isTakingItem = false;
            _itemRigidbody = null;
        }
    }
}