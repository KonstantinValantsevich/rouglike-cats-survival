using Entities.EntityComponents.Movements;
using Entities.Interfaces;

namespace Entities.Projectiles
{
    public class Projectile : Entity
    {
        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);

            Health.HealthReachedMin += () => Destroy(gameObject);
            KillOnTime();
        }

        protected virtual void KillOnTime()
        {
            Destroy(gameObject, 2.0f);
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new ForwardMovement(baseMovementSpeed, transform, transform);
        }

        public override void PerformHit(Entity attackedEntity)
        {
            attackedEntity.Health.ChangeHealth(-Attacker.BaseAttackDamage);
        }
    }
}