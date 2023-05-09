using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        public SpriteRenderer renderer;

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new FollowPlayerMovement(baseMovementSpeed, transform, transform, Player);
        }

        protected override void Update()
        {
            base.Update();

            var rotation = transform.localEulerAngles.z;
            var scale = transform.localScale;
            if (rotation is >= 270 or < 90) {
                scale.y = 1;
            }
            else {
                scale.y = -1;
            }
            transform.localScale = scale;
        }

        public override void ColliderTouched(GameObject touchedGameObject)
        {
            if (!touchedGameObject.TryGetComponent<IAttackable>(out var component)) {
                return;
            }

            if (component is Enemy) {
                return;
            }

            component.PerformHit(this);
        }

        public override void PerformHit(Entity attackedEntity)
        {
            attackedEntity.Health.ChangeHealth(-Attacker.BaseAttackDamage);
        }
    }
}