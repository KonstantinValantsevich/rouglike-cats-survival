﻿using System;
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
    public abstract class Entity : MonoBehaviour, IAttackable
    {
        protected Health health;
        protected Movement movement;
        protected AttacksController attacksController;
        protected Inventory inventory;

        public float moveSpeed;

        public event Action<Entity> Destroyed;

        private List<ITickable> tickables;

        protected virtual void Start()
        {
            health = new Health(100, 0);
            movement = new NoMovement(3, transform);
            attacksController = new AttacksController(new List<Attack> {new NoAttack(0.25f, null, null)});
            inventory = new Inventory();

            UpdateTickables();

            health.HealthReachedMin += () => Destroy(gameObject);
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

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public abstract void PerformHit(Health attackedHealth);
    }
}