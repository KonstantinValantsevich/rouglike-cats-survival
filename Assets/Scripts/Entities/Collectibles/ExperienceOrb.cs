using System.Collections.Generic;
using Entities.EntityComponents;
using Entities.EntityComponents.Attacks;
using Entities.EntityComponents.Movements;

namespace Entities.Collectibles
{
    public class ExperienceOrb : Collectible
    {
        public int experienceAmount = 3;

        public override void CollectItem(Inventory inventory)
        {
            inventory.AddExperience(experienceAmount);
        }

        protected override void InitialiseComponents()
        {
            health = new Health(1, 0);
            movement = new NoMovement(3, transform, transform);
            attacksController = new AttacksController(new List<Attack>
                { new NoAttack(0.25f, null, null, this.player) });
            inventory = new Inventory(0);
        }
    }
}