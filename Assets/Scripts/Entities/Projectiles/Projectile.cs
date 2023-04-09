using Entities.EntityComponents;
using Entities.EntityComponents.Movements;

namespace Entities.Projectiles
{
    public class Projectile : Entity
    {
        protected override void Start()
        {
            base.Start();
            movement = new ForwardMovement(10, transform);

            UpdateTickables();

            Destroy(gameObject, 2.0f);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-health.MaxHealth);
        }
    }
}