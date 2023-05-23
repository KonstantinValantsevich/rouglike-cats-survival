using System;
using System.Collections.Generic;
using Entities.Collectibles;
using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Interfaces;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IAttackable, IAgeChangeable
    {
        public event Action<Entity> EntityKilled = delegate { };

#region Stats Fields

        [Header("Technical Settings")]
        public bool shouldKillOnFarFromPlayer = true;
        public float distanceToBeKilled = 10;

        [Header("Health Settings")]
        public float baseHealth;
        public float baseHealthChange;

        [Header("Inventory Settings")]
        public int baseLevel;

        [Header("Movement Settings")]
        public float baseMovementSpeed;

        [Header("Attack Settings")]
        public float baseAttackDamage;
        public List<Attack> attacksList;

        [Header("Age settings")]
        public List<Sprite> ageViews;

#endregion

        public Health Health;
        public Movement Movement;
        public Attacker Attacker;
        public Inventory Inventory;

        public new SpriteRenderer renderer;

        protected IPlayerState Player;

        private List<ITickable> tickables;

        private bool isInvisible;

        public virtual void Initialise(IPlayerState player)
        {
            Player = player;

            InitialiseComponents();

            UpdateTickables();

            Health.HealthReachedMin += () => EntityKilled.Invoke(this);
            Health.Damaged += amount => Debug.Log($"Entity: {name} was damaged by {amount} damage");
        }

        protected virtual void InitialiseComponents()
        {
            Health = new Health(baseHealth, baseHealthChange);
            Inventory = new Inventory(baseLevel);
            Attacker = new Attacker(attacksList, new List<Attack>(), baseAttackDamage, transform, Inventory.artefacts,
                Player);
            Movement = new NoMovement();
        }

        protected void UpdateTickables()
        {
            tickables = new List<ITickable> { Health, Movement, Attacker };
        }

        protected virtual void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var tickable in tickables) {
                tickable.Tick(deltaTime);
            }

            if (shouldKillOnFarFromPlayer && isInvisible &&
                Vector3.Distance(Player.Position, transform.position) >
                distanceToBeKilled + Player.CameraRectCircleRadius) {
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
            Health.Reset();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            ColliderTouched(col.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            ColliderTouched(col.gameObject);
        }

        public virtual void ColliderTouched(GameObject touchedGameObject)
        {
            if (!touchedGameObject.TryGetComponent<Entity>(out var entity)) {
                return;
            }

            if (entity is IAttackable attackable) {
                attackable.PerformHit(this);
            }

            if (entity is Collectible collectible) {
                collectible.CollectItem(Inventory);
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

        public abstract void PerformHit(Entity attackedEntity);

        public virtual void ChangeAge(AgeType age)
        {
            var ageNum = (int) age;
            if (ageNum > ageViews.Count) {
                ageNum = ageViews.Count - 1;
            }
            if (ageNum < 0) {
                return;
            }
            renderer.sprite = ageViews[ageNum];
        }
    }
}