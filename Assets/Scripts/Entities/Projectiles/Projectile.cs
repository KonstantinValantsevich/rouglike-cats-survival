using System.Collections.Generic;
using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;

namespace Entities.Projectiles
{
    public class Projectile : Entity
    {
        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            shouldKillOnFarFromPlayer = false;

            health.HealthReachedMin += () => Destroy(gameObject);

            Destroy(gameObject, 2.0f);
        }

        protected override void InitialiseComponents()
        {
            health = new Health(100, 0);
            movement = new ForwardMovement(3, transform, transform);
            attacksController = new AttacksController(new List<Attack>
                { new NoAttack(0.25f, null, null, this.player) });
            inventory = new Inventory(0);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-health.MaxHealth);
        }
    }
}