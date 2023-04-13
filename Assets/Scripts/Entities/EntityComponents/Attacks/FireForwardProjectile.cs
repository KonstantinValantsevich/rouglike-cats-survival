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

            var bullet = Instantiate(AttacksPrefabs[0], attackerTransform.position, attackerTransform.rotation);
            bullet.gameObject.layer = LayerMask.NameToLayer("Player Attack");
            bullet.baseAttackDamage *= baseDamage * AttackMultiplier;
            bullet.Initialise(player);

            return true;
        }
    }
}