using System.Collections.Generic;
using Entities.EntityComponents.Interfaces;

namespace Entities.EntityComponents.Attacks
{
    public class AttacksController : ITickable
    {
        private List<Attack> attacks;

        public AttacksController(List<Attack> attacks)
        {
            this.attacks = attacks;
        }

        public void Tick(float deltaTime)
        {
            foreach (var attack in attacks)
            {
                attack.PerformAttack(deltaTime);
            }
        }
    }
}