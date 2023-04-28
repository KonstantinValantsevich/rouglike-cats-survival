using Entities.EntityComponents;
using UnityEngine;

namespace Entities.Collectibles
{
    public abstract class Collectible : Entity
    {
        [Header("Collectibles Settings")]
        public int spawnPriority;

        public override void PerformHit(Entity attackedEntity)
        {
        }

        public abstract void CollectItem(Inventory inventory);
    }
}