using UnityEngine;
using UnityEngine.Events;

namespace Door
{
    public class DoorScanner : MonoBehaviour
    {
        public event UnityAction OnOpen;
        public event UnityAction OnClose;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                OnOpen?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
                OnClose?.Invoke();
        }
    }
}