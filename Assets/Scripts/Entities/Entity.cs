using System;
using Entities.EntityComponents;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IAttackable
    {
        protected Health health;
        protected Movement movement;

        public float moveSpeed;

        public event Action<Entity> Destroyed;

        protected virtual void Start()
        {
            health = new Health(100, 0);
            movement = new NoMovement(3, transform);
            health.HealthReachedMin += () => Destroy(gameObject);
        }

        protected virtual void Update()
        {
            var deltaTime = Time.deltaTime;
            health.Tick(deltaTime);
            movement.Tick(deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            ColliderTouched(col.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            ColliderTouched(col.gameObject);
        }

        protected virtual void ColliderTouched(GameObject touchedGameObject)
        {
            if (!touchedGameObject.TryGetComponent<IAttackable>(out var component))
            {
                return;
            }

            component.PerformHit(health);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public abstract void PerformHit(Health attackedHealth);
    }
}