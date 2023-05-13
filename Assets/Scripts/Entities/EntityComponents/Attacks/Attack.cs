using System;
using System.Collections.Generic;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    [Serializable]
    public class AttackTierConfig
    {
        public float attackMultiplier;
        public Entity attackPrefab;
        public float cooldown;
    }

    public abstract class Attack : MonoBehaviour
    {
        public List<AttackTierConfig> AttackTiers;
        protected Transform attackerTransform;
        protected IPlayerState player;

        protected float AttackMultiplier;
        protected Entity AttackPrefab;
        private float cooldown;

        private float timePerformed;
        private float timeElapsed;

        private int currentAttackTier;

        public virtual void Initialize(Transform attackerTransform, IPlayerState player)
        {
            this.player = player;
            this.attackerTransform = attackerTransform;
            timePerformed = 0;
            ChangeAttackTier(1);
        }

        [ContextMenu("Increase Attack Tier")]
        public void IncreaseAttackTier()
        {
            ChangeAttackTier(currentAttackTier + 1);
        }

        public void ChangeAttackTier(int attackTier)
        {
            if (attackTier > AttackTiers.Count) {
                Debug.LogError(
                    $"[Attack] Trying to increase Tier in {name} to {attackTier} but max tier is{AttackTiers.Count}");
                return;
            }

            currentAttackTier = attackTier;
            var attackSettings = AttackTiers[currentAttackTier - 1];
            cooldown = attackSettings.cooldown;
            AttackMultiplier = attackSettings.attackMultiplier;
            AttackPrefab = attackSettings.attackPrefab;
        }

        public abstract void PerformAttack(float deltaTime, float baseDamage);

        protected virtual void InitialiseBullet(Entity bullet, float baseDamage)
        {
            bullet.baseAttackDamage *= baseDamage * AttackMultiplier;
            bullet.Initialise(player);
        }
        
        protected bool Cooldown(float deltaTime)
        {
            timeElapsed += deltaTime;
            if (timeElapsed - timePerformed < cooldown) {
                return true;
            }

            timePerformed = timeElapsed;
            return false;
        }
    }
}