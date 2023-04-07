using System.Threading.Tasks;
using Projectiles;
using UnityEngine;

namespace Player
{
    public class PlayerMain : Entity, IPlayerState
    {
        public float health;
        public float baseDamageMultiplier;
        public float regenMultiplier;

        public Projectile bulletPrefab;

        private Camera cameraMain;

        public Vector3 Position => transform.position;

        private void Start()
        {
            cameraMain = Camera.main;
            Shoot();
        }

        private void Update()
        {
            var mousePosition = Input.mousePosition;
            mousePosition = cameraMain.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            SetLookRotation(mousePosition);

            Move(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }

        private async void Shoot()
        {
            while (true)
            {
                await Task.Delay(250);
                var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.wasPlayerCreated = true;
            }
        }
    }
}