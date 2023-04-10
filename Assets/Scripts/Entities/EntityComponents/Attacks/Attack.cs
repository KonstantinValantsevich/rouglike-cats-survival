using System.Collections.Generic;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public abstract class Attack
    {
        protected List<Entity> attacksPrefabs;
        protected Transform transform;
        protected IPlayerState player;

        private float cooldown;
        private float timePerformed;
        private float timeElapsed;


        public Attack(float cooldown, List<Entity> prefabs, Transform transform, IPlayerState player)
        {
            this.player = player;
            this.cooldown = cooldown;
            attacksPrefabs = prefabs;
            this.transform = transform;
            timePerformed = 0;
        }

        public virtual bool PerformAttack(float deltaTime)
        {
            timeElapsed += deltaTime;
            if (timeElapsed - timePerformed < cooldown)
            {
                return false;
            }

            timePerformed = timeElapsed;
            return true;
        }
    }
}