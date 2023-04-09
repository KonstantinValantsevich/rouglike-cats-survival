using Entities.EntityComponents.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents
{
    public abstract class Movement : ITickable
    {
        public float movementSpeed;
        protected Transform transform;

        protected Movement(float movementSpeed, Transform transform)
        {
            this.movementSpeed = movementSpeed;
            this.transform = transform;
        }

        public abstract void Tick(float deltaTime);

        protected void Move(Vector2 movementDirection)
        {
            var velocity = movementSpeed * movementDirection.normalized;
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        protected void SetLookRotation(Vector3 pointToLook)
        {
            var lookDirection = pointToLook - transform.position;
            var angleToRotate = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);
        }
    }
}