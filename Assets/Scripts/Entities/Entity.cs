using System;
using System.Collections.Generic;
using Entities.Collectibles;
using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Interfaces;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using Entities.Projectiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IAttackable
    {
        public event Action<Entity> EntityKilled = delegate { };

        public bool shouldKillOnFarFromPlayer = true;

        [FormerlySerializedAs("farDistanceToKill")]
        public float distanceFromKill = 10;

        protected Health health;
        protected Movement movement;
        protected AttacksController attacksController;
        protected Inventory inventory;
        protected IPlayerState player;

        public IHealth Health => health;

        private List<ITickable> tickables;

        private bool isInvisible;

        public virtual void Initialise(IPlayerState player)
        {
            this.player = player;

            health = new Health(100, 0);
            movement = new NoMovement(3, transform, transform);
            attacksController = new AttacksController(new List<Attack>
                {new NoAttack(0.25f, null, null, this.player)});
            inventory = new Inventory(0);

            UpdateTickables();

            health.HealthReachedMin += () => EntityKilled.Invoke(this);
        }

        protected void UpdateTickables()
        {
            tickables = new List<ITickable> {health, movement, attacksController};
        }

        protected virtual void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var tickable in tickables)
            {
                tickable.Tick(deltaTime);
            }

            if (shouldKillOnFarFromPlayer && isInvisible &&
                Vector3.Distance(player.Position, transform.position) >
                distanceFromKill + player.CameraRectCircleRadius)
            {
                Debug.Log("Entity killed bcs invisible");
                EntityKilled.Invoke(this);
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }


        public void Deactivate()
        {
            gameObject.SetActive(false);
            health.Reset();
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
            if (!touchedGameObject.TryGetComponent<Entity>(out var entity))
            {
                return;
            }

            if (entity is IAttackable attackable)
            {
                attackable.PerformHit(health);
            }

            if (entity is Collectible collectible)
            {
                collectible.CollectItem(inventory);
            }
        }

        private void OnBecameInvisible()
        {
            isInvisible = true;
        }

        private void OnBecameVisible()
        {
            isInvisible = false;
        }

        public abstract void PerformHit(Health attackedHealth);
    }
}