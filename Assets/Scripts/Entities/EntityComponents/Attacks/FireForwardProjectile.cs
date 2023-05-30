using Entities.Projectiles;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class FireForwardProjectile : Attack
    {
        public override void PerformAttack(float deltaTime, float baseDamage)
        {
            if (Cooldown(deltaTime)) {
                return;
            }

            var bullet = Instantiate(AttackPrefab, attackerTransform.position, attackerTransform.rotation);
            InitialiseBullet(bullet, baseDamage);
        }
    }
}