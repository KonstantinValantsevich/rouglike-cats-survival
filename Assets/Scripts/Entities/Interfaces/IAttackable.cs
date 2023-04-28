using Entities.EntityComponents;

namespace Entities.Interfaces
{
    public interface IAttackable
    {
        public void PerformHit(Entity attackedEntity);
    }
}