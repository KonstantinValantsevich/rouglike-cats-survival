using System.Collections.Generic;
using Entities.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities.EntityComponents.Attacks
{
    public abstract class Attack : MonoBehaviour
    {
        public float attackMultiplier;
        public List<Entity> attacksPrefabs;
        [SerializeField]
        private float cooldown;

        protected Transform attackerTransform;
        protected IPlayerState player;
        
        private float timePerformed;
        private float timeElapsed;

        public virtual void Initialize(Transform attackerTransform, IPlayerState player)
        {
            this.player = player;
            this.attackerTransform = attackerTransform;
            timePerformed = 0;
        }

        public virtual bool PerformAttack(float deltaTime, float baseDamage)
        {
            timeElapsed += deltaTime;
            if (timeElapsed - timePerformed < cooldown) {
                return false;
            }

            timePerformed = timeElapsed;
            return true;
        }
    }
}