using System.Collections.Generic;
using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class NoAttack : Attack
    {
        public override bool PerformAttack(float deltaTime)
        {
            return false;
        }

        public NoAttack(float cooldown, List<Entity> prefabs, Transform transform, IPlayerState player) : base(cooldown,
            prefabs, transform, player)
        {
        }
    }
}