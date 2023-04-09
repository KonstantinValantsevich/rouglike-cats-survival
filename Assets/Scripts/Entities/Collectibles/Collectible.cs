using Entities.EntityComponents;

namespace Entities.Collectibles
{
    public abstract class Collectible : Entity
    {
        public int spawnPriority;
        
        public override void PerformHit(Health attackedHealth)
        {
        }

        public abstract void CollectItem(Inventory inventory);
    }
}