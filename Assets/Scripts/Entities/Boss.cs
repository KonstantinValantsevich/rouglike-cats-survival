using System.Collections.Generic;
using Entities.Collectibles;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using UI;

namespace Entities
{
    public class Boss : Enemy
    {
        public HealthBar healthBar;

        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            EntityKilled += ent => Destroy(ent.gameObject);
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();
            Attacker = new Attacker(new List<Attack>(), attacksList, baseAttackDamage, transform, new HashSet<ArtefactType>(), Player);
            Movement = new LookAtPlayer(baseMovementSpeed, transform, transform, Player);
            healthBar.Initialise(Health);
        }
    }
}