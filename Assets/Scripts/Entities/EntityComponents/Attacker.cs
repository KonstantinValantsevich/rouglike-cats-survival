using System.Collections.Generic;
using System.Linq;
using Entities.EntityComponents.Interfaces;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class Attacker : ITickable

    {
        public Dictionary<string, Attack> availibleAttacks;
        private List<Attack> attacksList = new List<Attack>();
        private float baseAttackDamage;

        private IPlayerState player;
        private Transform attackerTransform;

        public float BaseAttackDamage => baseAttackDamage;

        public Attacker(List<Attack> attacksList, float baseAttackDamage, Transform attackerTransform,
            IPlayerState player)
        {
            availibleAttacks = attacksList.ToDictionary(at => at.name, at => at);
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

            availibleAttacks.Remove(attackName);
            var attack = Object.Instantiate(attackToAdd, attackerTransform);
            attack.Initialize(attackerTransform, player);
            attacksList.Add(attack);
        }
    }
}