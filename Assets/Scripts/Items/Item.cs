using UnityEngine;

namespace Items
{
    public class Item : MonoBehaviour, IItem
    {
        public bool Taken { get; private set; } = false;

        public void Take()
        {
            Taken = true;
        }

        public void Drop()
        {
            Taken = false;
        }
    }
}