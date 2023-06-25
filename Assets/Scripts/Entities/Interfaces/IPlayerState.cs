using UnityEngine;

namespace Entities.Interfaces
{
    public interface IPlayerState
    {
        public Vector3 Position { get; }
        public Transform Transform { get; }

        public Rect CameraRect { get; }
        public float CameraRectCircleRadius => Mathf.Max(CameraRect.width, CameraRect.height);
    }
}