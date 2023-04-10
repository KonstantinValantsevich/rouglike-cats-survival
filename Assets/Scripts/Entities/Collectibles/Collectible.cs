using Entities.EntityComponents;
using Entities.Interfaces;

namespace Entities.Collectibles
{
    public abstract class Collectible : Entity
    {
        public int spawnPriority;

        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            shouldKillOnFarFromPlayer = true;
            distanceFromKill = 10;
        }

        public override void PerformHit(Health attackedHealth)
        {
        }

        public abstract void CollectItem(Inventory inventory);
    }
}