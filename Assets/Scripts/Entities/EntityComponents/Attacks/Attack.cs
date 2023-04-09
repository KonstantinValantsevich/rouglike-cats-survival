using System.Collections.Generic;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public abstract class Attack
    {
        protected List<GameObject> attacksPrefabs;
        protected Transform transform;


        private float cooldown;
        private float timePerformed;
        private float timeElapsed;

        public Attack(float cooldown, List<GameObject> prefabs, Transform transform)
        {
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