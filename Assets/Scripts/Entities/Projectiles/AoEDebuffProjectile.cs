using System.Collections.Generic;
using System.Linq;
using Debuffs;

namespace Entities.Projectiles
{
    public class AoEDebuffProjectile : AoEProjectile
    {
        public List<DebuffS> debuffsList;
        private List<Debuff> debuffs;

        public override void Explode(Enemy enemy)
        {
            debuffs = debuffsList.Select(DebuffExtension.GetDebuff).ToList();
            base.Explode(enemy);
            foreach (var debuff in debuffs) {
                debuff.ApplyDebuff(enemy);
            }
        }
    }

}