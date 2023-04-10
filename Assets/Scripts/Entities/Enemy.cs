using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);

            shouldKillOnFarFromPlayer = false;
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