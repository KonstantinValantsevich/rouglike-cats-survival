using System.Collections.Generic;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;

namespace Entities
{
    public class Boss : Enemy
    {
        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();
            Attacker = new Attacker(new List<Attack>(), attacksList, baseAttackDamage, transform, Player);
            Movement = new LookAtPlayer(baseMovementSpeed, transform, transform, Player);
        }
    }
}