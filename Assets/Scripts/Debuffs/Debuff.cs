using System;
using System.Threading.Tasks;
using DG.Tweening;
using Entities;
using Entities.EntityComponents.Movements;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Debuffs
{
    [Serializable]
    public class DebuffS
    {
        public float value;
        public DebuffEnum debuff;
    }

    public abstract class Debuff
    {
        public float value;
        public abstract void ApplyDebuff(Entity entity);
    }

    public class SlowdownDebuff : Debuff
    {
        public override void ApplyDebuff(Entity entity)
        {
            entity.Movement.ChangeMovementSpeed(-value);
        }
    }

    public class GravityDebuff : Debuff
    {
        public override async void ApplyDebuff(Entity entity)
        {
            GameObject explosion = null;
            for (int i = 0; i < 10 && explosion == null; i++) {
                await Task.Yield();
                explosion = GameObject.FindWithTag("Gravity Explosion");
            }
            if (explosion == null) {
                Debug.LogWarning("explosion is null");
                return;
            }
            var prevMovement = entity.Movement;
            entity.Movement = new FollowTargetMovement(explosion.transform, entity.baseMovementSpeed * value,
                entity.transform,
                entity.transform);
            DOVirtual.DelayedCall(value, () => {
                if (entity == null) {
                    return;
                }
                entity.Movement = prevMovement;
            });
        }
    }

    public class DamageDebuff : Debuff
    {
        public override void ApplyDebuff(Entity entity)
        {
            entity.Health.AddHealthChanger(-value);
        }
    }

    public class StunDebuff : Debuff
    {
        public override void ApplyDebuff(Entity entity)
        {
            var previousMovement = entity.Movement;
            DOVirtual.DelayedCall(value, () => {
                entity.Movement = previousMovement;
            }).OnStart(() => entity.Movement = new NoMovement());
        }
    }

    public enum DebuffEnum
    {
        Slowdown,
        DamageDebuff,
        Stun,
        Gravity
    }

    public static class DebuffExtension
    {
        public static Debuff GetDebuff(this DebuffS debuff)
        {
            return debuff.debuff switch {
                DebuffEnum.Slowdown => new SlowdownDebuff { value = debuff.value },
                DebuffEnum.DamageDebuff => new DamageDebuff { value = debuff.value },
                DebuffEnum.Stun => new StunDebuff { value = debuff.value },
                DebuffEnum.Gravity => new GravityDebuff() { value = debuff.value },
                _ => throw new ArgumentOutOfRangeException(nameof(debuff), debuff, null)
            };
        }
    }
}