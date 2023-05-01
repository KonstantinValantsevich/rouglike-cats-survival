using Entities.EntityComponents.Movements;

namespace Entities.Projectiles
{
    public class MegaProjectile : AoEDebuffProjectile
    {
        protected override void KillOnTime()
        {
            Destroy(gameObject);
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new NoMovement();
        }
    }
}