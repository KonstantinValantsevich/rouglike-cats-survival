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

        public Rect cameraRect
        {
            get
            {
                var cameraPosition = mainCamera.transform.position;
                var height = mainCamera.orthographicSize;
                var width = height * mainCamera.aspect;
                return new Rect(cameraPosition.x, cameraPosition.y, width, height);
            }
        }

        private Camera mainCamera;

        protected override void Start()
        {
            base.Start();
            mainCamera = Camera.main;
            movement = new PlayerMovement(5, transform, mainCamera);
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