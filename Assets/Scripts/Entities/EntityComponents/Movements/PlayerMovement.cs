using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class PlayerMovement : Movement
    {
        private readonly Camera camera;

        public PlayerMovement(float movementSpeed, Transform transform, Camera camera) : base(movementSpeed, transform)
        {
            this.camera = camera;
        }

        public override void Tick(float deltaTime)
        {
            var mousePosition = Input.mousePosition;
            mousePosition = camera.ScreenToWorldPoint(mousePosition);
            SetLookRotation(mousePosition);

            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }
    }
}