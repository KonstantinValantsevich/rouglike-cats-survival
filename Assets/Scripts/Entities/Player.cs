using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using Entities.Projectiles;
using UnityEngine;

namespace Entities
{
    public class Player : Entity, IPlayerState
    {
        public float cooldown = 0.25f;

        private float lastFireTime = 0;

        public Projectile bulletPrefab;

        public Vector3 Position => transform.position;

        protected override void Start()
        {
            base.Start();

            movement = new PlayerMovement(5, transform, Camera.main);
            Shoot();
        }

        protected override void Update()
        {
            base.Update();

            Shoot();
        }

        private void Shoot()
        {
            if (Time.time - lastFireTime < cooldown)
            {
                return;
            }

            lastFireTime = Time.time;
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.gameObject.layer = LayerMask.NameToLayer("Player Attack");
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-attackedHealth.MaxHealth);
        }
    }
}