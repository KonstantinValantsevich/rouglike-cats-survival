using System.Collections.Generic;
using UnityEngine;

namespace Entities.EntityComponents.Attacks
{
    public class NoAttack : Attack
    {
        public override bool PerformAttack(float deltaTime)
        {
            return false;
        }

        public NoAttack(float cooldown, List<GameObject> prefabs, Transform transform) : base(cooldown, prefabs,
            transform)
        {
        }
    }
}