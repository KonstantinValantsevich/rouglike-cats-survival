using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class ForwardMovement : Movement
    {
        public ForwardMovement(float movementSpeed, Transform transform) : base(movementSpeed, transform)
        {
        }

        public override void Tick(float deltaTime)
        {
            Move(transform.right);
        }
    }
}