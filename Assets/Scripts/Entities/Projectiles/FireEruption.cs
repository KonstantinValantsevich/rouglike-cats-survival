using System.Collections.Generic;
using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;
using UnityEngine;

namespace Entities.Projectiles
{
    public class FireEruption : Fire
    {
        private Vector3 maxScale;
        private Vector3 deltaScale;
        public Vector3 minScale;

        protected override void InitialiseComponents()
        {
            Health = new Health(baseHealth, baseHealthChange);
            Inventory = new Inventory(baseLevel);
            Attacker = new Attacker(attacksList, new List<Attack>(), baseAttackDamage, transform, Player);
            Movement = new NoMovement();

            maxScale = transform.localScale;
            transform.localScale = minScale;
            deltaScale = new Vector3(rotationSpeed, rotationSpeed, 0);
            Destroy(gameObject, ((maxScale - minScale) / rotationSpeed * 2).x * cyclesCount);
        }

        //TODO: Вынести в Movement Component
        protected override void Update()
        {
            transform.localScale += deltaScale * Time.deltaTime;
            if (transform.localScale.sqrMagnitude > maxScale.sqrMagnitude ||
                transform.localScale.sqrMagnitude < minScale.sqrMagnitude) {
                deltaScale *= -1;
            }
        }
    }
}