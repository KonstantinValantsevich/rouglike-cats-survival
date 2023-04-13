using System.Collections.Generic;
using Entities.EntityComponents.Interfaces;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class Attacker : ITickable

    {
        private List<Attack> attacksList;
        private float baseAttackDamage;

        public float BaseAttackDamage => baseAttackDamage;

        public Attacker(List<Attack> attacksList, float baseAttackDamage, Transform attackerTransform, IPlayerState player)
        {
            this.attacksList = attacksList;
            this.baseAttackDamage = baseAttackDamage;

            foreach (var attack in attacksList) {
                attack.Initialize(attackerTransform, player);
            }
        }

        public void Tick(float deltaTime)
        {
            foreach (var attack in attacksList) {
                attack.PerformAttack(deltaTime, baseAttackDamage);
            }
        }
    }
}