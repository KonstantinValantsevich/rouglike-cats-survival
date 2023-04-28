using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class RotationMovement : Movement
    {
        private float rotationSpeed;

        public RotationMovement(float rotationAngle, float movementSpeed, float rotationSpeed,
            Transform movementTransform, Transform rotationTransform)
            : base(movementSpeed, movementTransform, rotationTransform)
        {
            this.rotationSpeed = rotationSpeed;
            rotationTransform.Rotate(Vector3.forward, -rotationAngle / 2, Space.Self);
        }

        public override void Tick(float deltaTime)
        {
            rotationTransform.Rotate(Vector3.forward, rotationSpeed * deltaTime, Space.Self);
        }
    }
}