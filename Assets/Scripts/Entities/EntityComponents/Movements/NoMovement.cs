using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class NoMovement : Movement
    {
        public NoMovement() : base(0, null, null)
        {
        }

        public override void Tick(float deltaTime)
        {
        }
    }
}