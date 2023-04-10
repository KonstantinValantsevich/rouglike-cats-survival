using System.Collections.Generic;
using Entities.Interfaces;
using Entities.Projectiles;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class FireForwardProjectile : Attack
    {
        public override bool PerformAttack(float deltaTime)
        {
            if (!base.PerformAttack(deltaTime))
            {
                return false;
            }

            var bullet = Object.Instantiate(attacksPrefabs[0], transform.position, transform.rotation);
            bullet.gameObject.layer = LayerMask.NameToLayer("Player Attack");
            bullet.Initialise(player);
            
            return true;
        }

        public FireForwardProjectile(float cooldown, List<Entity> prefabs, Transform transform, IPlayerState player)
            : base(cooldown,
                prefabs, transform, player)
        {
        }
    }
}