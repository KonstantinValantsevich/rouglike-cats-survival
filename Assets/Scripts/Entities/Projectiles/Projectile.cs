using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Projectiles
{
    public class Projectile : Entity
    {
        public UnityEvent HitEvent;
        public GameObject model;

        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            Health.HealthReachedMin += () => {
                enabled = false;
                if (renderer != null) {
                    renderer.enabled = false;
                }
                else if (model != null) {
                    model.SetActive(false);
                }
                HitEvent.Invoke();
                Destroy(gameObject, 1.0f);
            };
            KillOnTime();
        }

        protected virtual void KillOnTime()
        {
            Destroy(gameObject, 2.0f);
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new ForwardMovement(baseMovementSpeed, transform, transform);
        }

        public override void PerformHit(Entity attackedEntity)
        {
            attackedEntity.Health.ChangeHealth(-Attacker.BaseAttackDamage);
            HitEvent.Invoke();
        }
    }
}