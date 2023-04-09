using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using UnityEngine;

namespace Entities.Projectiles
{
    public class Projectile : Entity
    {
        protected override void Start()
        {
            base.Start();
            movement = new ForwardMovement(10, transform);

            Destroy(gameObject, 2.0f);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-health.MaxHealth);
        }
    }
}