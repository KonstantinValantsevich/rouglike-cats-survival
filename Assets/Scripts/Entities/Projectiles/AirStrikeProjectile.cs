using DG.Tweening;
using Entities.EntityComponents.Movements;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Entities.Projectiles
{
    public class AirStrikeProjectile : AoEDebuffProjectile
    {
        [Header("Air Strike")]
        public float timeToFall;
        public Vector3 startOffset;
        public Vector3 startScale;

        protected override void KillOnTime()
        {
            DOVirtual.DelayedCall(timeToFall, () => Health.ChangeHealth(-Health.CurrentHealth));
        }

        protected override void InitialiseComponents()
        {
            base.InitialiseComponents();

            Movement = new AirAttackMovement(timeToFall, startOffset, startScale, transform, transform);
        }
    }
}