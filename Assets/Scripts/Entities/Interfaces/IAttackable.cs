using Entities.EntityComponents;

namespace Entities.Interfaces
{
    public interface IAttackable
    {
        public void PerformHit(Health attackedHealth);
    }
}