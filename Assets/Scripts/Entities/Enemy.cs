using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new FollowPlayerMovement(baseMovementSpeed, transform, transform, Player);
        }

        protected override void ColliderTouched(GameObject touchedGameObject)
        {
            if (!touchedGameObject.TryGetComponent<IAttackable>(out var component)) {
                return;
            }

            if (component is Enemy) {
                return;
            }

            component.PerformHit(Health);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-Attacker.BaseAttackDamage);
        }
    }
}