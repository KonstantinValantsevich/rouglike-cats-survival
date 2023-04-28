using System;
using DG.Tweening;
using Entities;
using Entities.EntityComponents.Movements;

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
                if (entity == null) {
                    return;
                }
                entity.Movement = previousMovement;
            }).OnStart(() => entity.Movement = new NoMovement());
        }
    }

    public enum DebuffEnum
    {
        Slowdown,
        DamageDebuff,
        Stun
    }

    public static class DebuffExtension
    {
        public static Debuff GetDebuff(this DebuffS debuff)
        {
            return debuff.debuff switch {
                DebuffEnum.Slowdown => new SlowdownDebuff { value = debuff.value },
                DebuffEnum.DamageDebuff => new DamageDebuff { value = debuff.value },
                DebuffEnum.Stun => new StunDebuff { value = debuff.value },
                _ => throw new ArgumentOutOfRangeException(nameof(debuff), debuff, null)
            };
        }
    }
}