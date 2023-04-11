using System;

namespace Entities.EntityComponents.Interfaces
{
    public interface IHealth
    {
        public float CurrentHealth { get; }
        public float CurrentHealthChanger { get; }
        public float MaxHealth { get; }

        public const float MinHealth = 0;

        public event Action<float> Healed;
        public event Action<float> Damaged;
    }
}