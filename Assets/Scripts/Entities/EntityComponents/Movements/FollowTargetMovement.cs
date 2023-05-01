using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class FollowTargetMovement : Movement
    {
        private Transform target;

        public FollowTargetMovement(Transform target, float baseMovementSpeed, Transform transform,
            Transform rotateTransform) : base(
            baseMovementSpeed, transform, rotateTransform)
        {
            this.target = target;
        }

        public override void Tick(float deltaTime)
        {
            SetLookRotation(target.transform.position);
            Move(movementTransform.right);
        }
    }
}