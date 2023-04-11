using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class ForwardMovement : Movement
    {
        public ForwardMovement(float movementSpeed, Transform movementTransform, Transform rotationTransform) : base(
            movementSpeed, movementTransform, rotationTransform)
        {
        }

        public override void Tick(float deltaTime)
        {
            Move(movementTransform.right);
        }
    }
}