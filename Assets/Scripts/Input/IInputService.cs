using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Input
{
    public interface IInputService
    {
        public Vector2 AxisMovement { get; }

        public Vector2 AxisRotation { get; }

        public Vector2 TouchPoint { get; }

        public bool Touched { get; }
    }
}