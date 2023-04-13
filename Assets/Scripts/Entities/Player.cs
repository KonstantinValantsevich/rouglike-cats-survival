using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using UI;
using UnityEngine;

namespace Entities
{
    public class Player : Entity, IPlayerState
    {
        [Header("Player References")]
        public HealthBar playerHealthBar;
        public Transform playerModel;

        public Vector3 Position => transform.position;

        public Rect CameraRect {
            get {
                var cameraPosition = mainCamera.transform.position;
                var height = mainCamera.orthographicSize * 2;
                var width = height * mainCamera.aspect;
                var rect = new Rect {
                    width = width,
                    height = height,
                    center = cameraPosition
                };
                return rect;
            }
        }

        private Camera mainCamera;

        public override void Initialise(IPlayerState player)
        {
            mainCamera = Camera.main;

            base.Initialise(player);

            playerHealthBar.Initialise(Health);
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new PlayerMovement(baseMovementSpeed, transform, playerModel, mainCamera);
            Attacker = new Attacker(attacksList, baseAttackDamage, playerModel, Player);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-attackedHealth.MaxHealth);
        }
    }
}