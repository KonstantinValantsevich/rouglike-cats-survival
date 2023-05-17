using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Collectibles;
using Entities.EntityComponents.Interfaces;
using Entities.Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Entities.EntityComponents.Attacks
{
    public class Attacker : ITickable, IAgeChangeable

    {
        public Dictionary<string, Attack> availibleAttacks;
        private List<Attack> attacksList;
        private float baseAttackDamage;

        private IPlayerState player;
        private Transform attackerTransform;
        private HashSet<ArtefactType> artefacts;
        public float BaseAttackDamage => baseAttackDamage;

        private AgeType age;

        public Attacker(List<Attack> attacksList, List<Attack> activeAttacks, float baseAttackDamage,
            Transform attackerTransform, HashSet<ArtefactType> artefacts,
            IPlayerState player)
        {
            this.artefacts = artefacts;
            availibleAttacks = attacksList.ToDictionary(at => at.name, at => at);
            this.attacksList = activeAttacks;
            foreach (var attack in activeAttacks) {
                attack.Initialize(attackerTransform, player);
            }
            this.baseAttackDamage = baseAttackDamage;
            this.player = player;
            this.attackerTransform = attackerTransform;
        }

        public void Tick(float deltaTime)
        {
            foreach (var attack in attacksList) {
                attack.PerformAttack(deltaTime, baseAttackDamage);
            }
        }

        public void AddAttack(string attackName)
        {
            var attackToAdd = availibleAttacks[attackName];
            if (attackToAdd == null) {
                Debug.LogError($"No attack {{{attackName}}} in available attacks");
            }

            if (attacksList.Contains(attackToAdd)) {
                attackToAdd.IncreaseAttackTier();
            }
            else {
                var attack = Object.Instantiate(attackToAdd, attackerTransform);
                attack.Initialize(attackerTransform, player);
                attacksList.Add(attack);
            }

            availibleAttacks.Remove(attackName);
            UpdateAvailibleAttacks();
        }

        public void ChangeAge(AgeType age)
        {
            this.age = age;
            UpdateAvailibleAttacks();
        }

        private void UpdateAvailibleAttacks()
        {
            foreach (var attack in attacksList) {
                if (artefacts.Contains(attack.attackSettings.artefactToUpgrade) &&
                    attack.currentAttackTier == (int) age) {
                    availibleAttacks[attack.name] = attack;
                }
            }
        }
    }
}