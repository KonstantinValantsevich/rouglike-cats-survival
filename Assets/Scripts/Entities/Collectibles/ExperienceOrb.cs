using Entities.EntityComponents;

namespace Entities.Collectibles
{
    public class ExperienceOrb : Collectible
    {
        public int experienceAmount = 3;

        public override void CollectItem(Inventory inventory)
        {
            inventory.AddExperience(experienceAmount);
        }
    }
}