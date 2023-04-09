using System.Collections.Generic;
using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using Entities.Projectiles;
using UnityEngine;

namespace Entities
{
    public class Player : Entity, IPlayerState
    {
        public Projectile bulletPrefab;

        public Vector3 Position => transform.position;

        protected override void Start()
        {
            base.Start();

            movement = new PlayerMovement(5, transform, Camera.main);
            attacksController =
                new AttacksController(new List<Attack>
                    {new FireForwardProjectile(0.25f, new List<GameObject> {bulletPrefab.gameObject}, transform)});

            UpdateTickables();
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-attackedHealth.MaxHealth);
        }
    }
}