using Entities.EntityComponents;
using Entities.Projectiles;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerMain : Entity, IPlayerState
    {
        public float cooldown = 0.25f;

        private float lastFireTime = 0;

        public Projectile bulletPrefab;

        private Camera cameraMain;

        public Vector3 Position => transform.position;

        protected override void Start()
        {
            base.Start();

            cameraMain = Camera.main;
            Shoot();
        }

        protected override void Update()
        {
            base.Update();

            var mousePosition = Input.mousePosition;
            mousePosition = cameraMain.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            SetLookRotation(mousePosition);

            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));

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

        public override void PerformAttack(HealthController attackedHealthController)
        {
            attackedHealthController.ChangeHealth(-attackedHealthController.MaxHealth);
        }
    }
}