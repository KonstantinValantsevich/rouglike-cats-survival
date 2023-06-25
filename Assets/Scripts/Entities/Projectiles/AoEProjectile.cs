using Entities.Interfaces;
using UnityEngine;

namespace Entities.Projectiles
{
    public class AoEProjectile : Projectile
    {
        [Header("AoE")]
        public float explosionRadius;
        public float explosionDamageMultiplier = 0.1f;
        public GameObject explosionPrefab;
        public float explosionTime = 0.5f;

        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            Health.HealthReachedMin += OnDestroyed;
        }

        private void OnDestroyed()
        {
            if (explosionRadius == 0) {
                return;
            }

            var hitList = Physics2D.OverlapCircleAll(transform.position, explosionRadius,
                LayerMask.GetMask("Enemy"));

            if (hitList.Length == 0) {
                return;
            }

            foreach (var hit in hitList) {
                Explode(hit.GetComponent<Enemy>());
            }

            var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.localScale = new Vector3(explosionRadius * 2, explosionRadius * 2, 1);
            Destroy(explosion, explosionTime);
        }

        public virtual void Explode(Enemy enemy)
        {
            enemy.Health.ChangeHealth(-Attacker.BaseAttackDamage * explosionDamageMultiplier);
        }
    }
}