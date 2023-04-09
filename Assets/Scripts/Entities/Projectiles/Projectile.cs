using Entities.EntityComponents;
using UnityEngine;

namespace Entities.Projectiles
{
    public class Projectile : Entity
    {
        protected override void Start()
        {
            base.Start();

            Destroy(gameObject, 2.0f);
        }

        protected override void Update()
        {
            base.Update();
            Move(transform.right);
        }

        public override void PerformAttack(HealthController attackedHealthController)
        {
            attackedHealthController.ChangeHealth(-healthController.MaxHealth);
        }
    }
}