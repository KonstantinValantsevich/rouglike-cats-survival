using System;
using Entities.EntityComponents;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IAttackable
    {
        protected HealthController healthController;

        public float moveSpeed;

        private Vector3 dir;
        public event Action<Entity> Destroyed;

        protected virtual void Start()
        {
            healthController = new HealthController(100, 0);
            healthController.HealthReachedMin += () => Destroy(gameObject);
        }

        protected virtual void Update()
        {
            healthController.Tick(Time.deltaTime);
        }

        protected void Move(Vector2 movementDirection)
        {
            var velocity = moveSpeed * movementDirection.normalized;
            transform.Translate(velocity * Time.deltaTime, Space.World);
        }

        protected void SetLookRotation(Vector3 pointToLook)
        {
            dir = pointToLook - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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

            component.PerformAttack(healthController);
        }

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public abstract void PerformAttack(HealthController attackedHealthController);
    }

    public interface IAttackable
    {
        public void PerformAttack(HealthController attackedHealthController);
    }
}