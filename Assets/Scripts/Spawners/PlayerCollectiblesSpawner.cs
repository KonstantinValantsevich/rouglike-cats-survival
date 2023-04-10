using Entities.Collectibles;
using UnityEngine;

namespace Spawners
{
    public class PlayerCollectiblesSpawner : Spawner<Collectible>
    {
        protected override Collectible SpawnEntity()
        {
            var collectible = base.SpawnEntity();
            collectible.gameObject.layer = LayerMask.NameToLayer("Player Collectibles");
            return collectible;
        }
    }
}