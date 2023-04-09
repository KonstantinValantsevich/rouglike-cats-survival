using Entities.EntityComponents;
using UnityEngine;

namespace Entities.Collectibles
{
    public class ExperienceOrb : Collectible
    {
        public int experienceAmount;

        public override void CollectItem(Inventory inventory)
        {
            Debug.Log("Collected!");
        }
    }
}