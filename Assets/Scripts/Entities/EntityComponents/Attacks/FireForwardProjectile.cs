using System.Collections.Generic;
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
            return true;
        }

        public FireForwardProjectile(float cooldown, List<GameObject> prefabs, Transform transform) : base(cooldown,
            prefabs, transform)
        {
        }
    }
}