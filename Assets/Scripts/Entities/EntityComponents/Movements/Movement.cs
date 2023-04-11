using Entities.EntityComponents.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents
{
    public abstract class Movement : ITickable
    {
        public const float minMovementSpeed = 0;

        private float movementSpeed;
        protected Transform movementTransform;
        protected Transform rotationTransform;

        protected Movement(float movementSpeed, Transform movementTransform, Transform rotationTransform)
        {
            this.movementSpeed = movementSpeed;
            this.movementTransform = movementTransform;
            this.rotationTransform = rotationTransform;
        }

        public abstract void Tick(float deltaTime);

        protected void Move(Vector2 movementDirection)
        {
            var velocity = movementSpeed * movementDirection.normalized;
            movementTransform.Translate(velocity * Time.deltaTime, Space.World);
        }

        protected void SetLookRotation(Vector3 pointToLook)
        {
            var lookDirection = pointToLook - rotationTransform.position;
            var angleToRotate = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            rotationTransform.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);
        }

        public void ChangeMovementSpeed(float amount)
        {
            movementSpeed += amount;
            movementSpeed = Mathf.Clamp(movementSpeed, 0, movementSpeed);
        }
    }
}