using Entities.EntityComponents.Movements;
using Entities.Interfaces;

namespace Entities.Projectiles
{
    public class AutoProjectile : AoEDebuffProjectile
    {
        private Entity target;

        public override void Initialise(IPlayerState player)
        {
            target = SceneHelper.FindRandomEnemy();
            if (target == null) {
                Destroy(gameObject);
                return;
            }

            base.Initialise(player);

            Health.HealthReachedMin += Destroy;

            target.EntityKilled += OnTargetKilled;
        }

        protected override void KillOnTime()
        {
        }

        private void Destroy()
        {
            Health.HealthReachedMin -= Destroy;
            target.EntityKilled -= OnTargetKilled;
            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
        }

        private void OnTargetKilled(Entity _)
        {
            Destroy();
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new FollowTargetMovement(target.transform, baseMovementSpeed, transform, transform);
        }
    }
}