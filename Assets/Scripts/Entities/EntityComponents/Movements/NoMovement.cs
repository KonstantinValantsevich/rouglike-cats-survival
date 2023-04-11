using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class NoMovement : Movement
    {
        public NoMovement(float movementSpeed, Transform movementTransform, Transform rotationTransform) : base(movementSpeed, movementTransform, rotationTransform)
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}