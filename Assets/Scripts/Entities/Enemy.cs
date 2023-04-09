using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using Unity.VisualScripting;
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

        protected override void Start()
        {
            base.Start();

            movement = new PlayerFollowMovement(3, transform, player);

            UpdateTickables();
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

            component.PerformHit(health);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(1);
        }
    }
}