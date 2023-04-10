using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;

namespace Entities.Projectiles
{
    public class Projectile : Entity
    {
        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            movement = new ForwardMovement(10, transform);
            shouldKillOnFarFromPlayer = false;

            UpdateTickables();

            health.HealthReachedMin += () => Destroy(gameObject);

            Destroy(gameObject, 2.0f);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-health.MaxHealth);
        }
    }
}