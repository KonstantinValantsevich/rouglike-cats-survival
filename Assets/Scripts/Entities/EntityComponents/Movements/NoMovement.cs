using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class NoMovement : Movement
    {
        public NoMovement(float movementSpeed, Transform transform) : base(movementSpeed, transform)
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}