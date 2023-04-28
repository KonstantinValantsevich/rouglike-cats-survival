using Entities.EntityComponents.Movements;

namespace Entities.Projectiles
{
    public class Torch : Entity
    {
        public float rotationSpeed;
        public float rotationAngle;

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();
            Movement = new RotationMovement(rotationAngle, baseMovementSpeed, rotationSpeed, transform, transform);
            Destroy(gameObject, rotationAngle / rotationSpeed);
        }

        public override void PerformHit(Entity attackedEntity)
        {
            attackedEntity.Health.ChangeHealth(-Attacker.BaseAttackDamage);
            attackedEntity.Health.AddHealthChanger(-Attacker.BaseAttackDamage * 0.1f);
        }
    }
}