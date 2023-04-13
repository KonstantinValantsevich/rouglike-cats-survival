using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class FireForwardProjectile : Attack
    {
        public override bool PerformAttack(float deltaTime, float baseDamage)
        {
            if (!base.PerformAttack(deltaTime, baseDamage)) {
                return false;
            }

            var bullet = Instantiate(attacksPrefabs[0], attackerTransform.position, attackerTransform.rotation);
            bullet.gameObject.layer = LayerMask.NameToLayer("Player Attack");
            bullet.baseAttackDamage *= baseDamage * attackMultiplier;
            bullet.Initialise(player);

            return true;
        }
    }
}