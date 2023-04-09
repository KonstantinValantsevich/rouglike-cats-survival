using UnityEngine;

namespace Entities.Interfaces
{
    public interface IPlayerState
    {
        public Vector3 Position { get; }

        /// <summary>
        /// Rect in world of camera view.
        /// </summary>
        public Rect cameraRect { get; }
    }
}