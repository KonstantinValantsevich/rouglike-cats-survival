using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class RotationMovement : Movement
    {
        private float rotationSpeed;

        private float halfRotation;

        public RotationMovement(float rotationAngle, float movementSpeed, float rotationSpeed,
            Transform movementTransform, Transform rotationTransform)
            : base(movementSpeed, movementTransform, rotationTransform)
        {
            this.rotationSpeed = rotationSpeed;
            halfRotation = rotationAngle / 2;
            rotationTransform.Rotate(Vector3.forward, -halfRotation, Space.Self);
        }

        public override void Tick(float deltaTime)
        {
            rotationTransform.Rotate(Vector3.forward, rotationSpeed * deltaTime, Space.Self);
            var currentRotation = rotationTransform.localEulerAngles.z;
            if (currentRotation >= 180 && currentRotation < 360 - halfRotation
                || currentRotation < 180 && currentRotation > halfRotation) {
                rotationSpeed *= -1;
            }
        }
    }
}