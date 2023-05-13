using System.Collections.Generic;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class FireCircleProjectile : Attack
    {
        private List<Entity> bullets;
        
        public override void PerformAttack(float deltaTime, float baseDamage)
        {
            if (Cooldown(deltaTime)) {
                return;
            }

            bullets = new List<Entity>();
            for (int i = 0; i < 8; i++) {
                var bullet = Instantiate(AttackPrefab, attackerTransform.position, attackerTransform.rotation);
                bullet.transform.Rotate(0, 0, 45 * i, Space.Self);
                InitialiseBullet(bullet, baseDamage);
                bullets.Add(bullet);
            }
        }
    }
}