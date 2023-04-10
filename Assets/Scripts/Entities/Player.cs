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

        public Rect CameraRect
        {
            get
            {
                var cameraPosition = mainCamera.transform.position;
                var height = mainCamera.orthographicSize * 2;
                var width = height * mainCamera.aspect;
                var rect = new Rect
                {
                    width = width,
                    height = height,
                    center = cameraPosition
                };
                return rect;
            }
        }

        private Rect cameraRect = Rect.zero;
        private Camera mainCamera;

        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            mainCamera = Camera.main;
            movement = new PlayerMovement(5, transform, mainCamera);
            attacksController =
                new AttacksController(new List<Attack>
                    {new FireForwardProjectile(0.25f, new List<Entity> {bulletPrefab}, transform, this)});

            UpdateTickables();
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-attackedHealth.MaxHealth);
        }
    }
}