using Cinemachine;
using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class PlayerMovement : Movement
    {
        private readonly Camera camera;
        private readonly CinemachineVirtualCamera virtCamera;

        private int movingTicks;
        private float size;

        public PlayerMovement(float movementSpeed, Transform movementTransform, Transform rotationTransform,
            Camera camera, CinemachineVirtualCamera virtualCamera) : base(movementSpeed, movementTransform,
            rotationTransform)
        {
            this.camera = camera;
            virtCamera = virtualCamera;
            movingTicks = 0;
            size = virtCamera.m_Lens.OrthographicSize;
        }

        public override void Tick(float deltaTime)
        {
            var mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);
            SetLookRotation(mousePosition);
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            movingTicks += x != 0 || y != 0 ? 1 : -movingTicks;

            if (movingTicks > 100) {
                virtCamera.m_Lens.OrthographicSize = size * 1.5f;
            }
            else {
                virtCamera.m_Lens.OrthographicSize = size;
            }

            Move(new Vector2(x, y));
        }
    }
}