using System.Threading.Tasks;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : Entity
    {
        public bool wasPlayerCreated;

        private async void Start()
        {
            await Task.Delay(2_000);
            Destroy(gameObject);
        }

        private void Update()
        {
            Move(transform.right);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.TryGetComponent<Enemy>(out var enemy)) return;
            Destroy(gameObject);
            if (wasPlayerCreated)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}