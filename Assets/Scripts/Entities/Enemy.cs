using System.Collections.Generic;
using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;
using Entities.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);

            shouldKillOnFarFromPlayer = false;
        }

        protected override void InitialiseComponents()
        {
            health = new Health(100, 0);
            movement = new FollowPlayerMovement(3, transform, transform, player);
            attacksController = new AttacksController(new List<Attack>
                { new NoAttack(0.25f, null, null, this.player) });
            inventory = new Inventory(0);
        }

        protected override void ColliderTouched(GameObject touchedGameObject)
        {
            if (!touchedGameObject.TryGetComponent<IAttackable>(out var component)) {
                return;
            }

            if (component is Enemy) {
                return;
            }

            component.PerformHit(health);
        }

        public override void PerformHit(Health attackedHealth)
        {
            attackedHealth.ChangeHealth(-1);
        }
    }
}