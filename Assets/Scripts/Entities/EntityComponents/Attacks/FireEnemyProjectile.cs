using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class FireEnemyProjectile : Attack
    {
        public override void PerformAttack(float deltaTime, float baseDamage)
        {
            if (Cooldown(deltaTime)) {
                return;
            }

            var enemy = SceneHelper.FindClosestEnemy(transform.position);
            if (enemy == null) {
                return;
            }

            var bullet = Instantiate(AttackPrefab, attackerTransform.position, Quaternion.identity);
            var lookDirection = enemy.transform.position - bullet.transform.position;
            var angleToRotate = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angleToRotate, Vector3.forward);           
            InitialiseBullet(bullet, baseDamage);
        }
    }
}