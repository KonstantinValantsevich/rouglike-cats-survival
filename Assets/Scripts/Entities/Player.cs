using System;
using System.Linq;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using TMPro;
using UI;
using UnityEngine;

namespace Entities
{
    public class Player : Entity, IPlayerState
    {
        [Header("Player References")]
        public HealthBar playerHealthBar;
        public Transform playerModel;

        [Header("UI")]
        public LevelUpScreen levelUpScreen;
        public ExpBar ExpBar;
        public Vector3 Position => transform.position;
        public TextMeshProUGUI artefactsList;

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
            ExpBar.Initialise(Inventory);
            levelUpScreen.AbilityChosen += Attacker.AddAttack;
            Inventory.ArtefactCollected += artefact => { artefactsList.text += $"\n   -{artefact}"; };
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new PlayerMovement(baseMovementSpeed, transform, playerModel, mainCamera);
            Attacker = new Attacker(attacksList, GetComponentsInChildren<Attack>().ToList(), baseAttackDamage,
                playerModel, Inventory.artefacts, Player);

            Inventory.LevelIncreased += OnLevelUp;
        }

        private void OnLevelUp()
        {
            var rng = new System.Random();
            var attacksToGet = Attacker.availibleAttacks.Select(pair => pair.Key).OrderBy(l => rng.Next()).ToList();
            Time.timeScale = 0;
            levelUpScreen.Initialize(attacksToGet.GetRange(0, Math.Min(attacksToGet.Count, 4)));
        }

        public override void PerformHit(Entity attackedEntity)
        {
            attackedEntity.Health.ChangeHealth(-attackedEntity.Health.MaxHealth);
        }

        public override void ChangeAge(AgeType age)
        {
            Attacker.ChangeAge(age);
        }
    }
}