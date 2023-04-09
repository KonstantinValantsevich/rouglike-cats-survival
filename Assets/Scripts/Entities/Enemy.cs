using Entities.EntityComponents;
using Entities.Player;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        private IPlayerState player;

        public void Init(IPlayerState player)
        {
            this.player = player;
        }

        protected override void Update()
        {
            SetLookRotation(player.Position);
            Move(transform.right);
        }

        protected override void ColliderTouched(GameObject touchedGameObject)
        {
            if (!touchedGameObject.TryGetComponent<IAttackable>(out var component))
            {
                return;
            }

            if (component is Enemy)
            {
                return;
            }

            component.PerformAttack(healthController);
        }

        public override void PerformAttack(HealthController attackedHealthController)
        {
            attackedHealthController.ChangeHealth(1);
        }
    }
}