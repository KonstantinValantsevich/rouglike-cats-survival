using System;
using Entities.EntityComponents.Interfaces;

namespace Entities.EntityComponents
{
    //TODO: Change system of health change to two methods!
    public class Health : ITickable, IHealth
    {
        public const float MinHealth = 0;

        public event Action<float> Healed = delegate { };
        public event Action<float> Damaged = delegate { };

        public event Action<float> HealthChangerChanged = delegate { };

        public event Action HealthReachedMin = delegate { };
        public event Action HealthReachedMax = delegate { };

        public float CurrentHealth => currentHealth;
        public float CurrentHealthChanger => currentHealthChanger;
        public float MaxHealth => maxHealth;

        private float currentHealth;
        private float currentHealthChanger;
        private readonly float maxHealth;
        private readonly float initialHealthChanger;

        public Health(float maxHealth, float healthChanger)
        {
            this.maxHealth = currentHealth = maxHealth;
            initialHealthChanger = currentHealthChanger = healthChanger;
        }

        public void Tick(float deltaTime)
        {
            ChangeHealth(currentHealthChanger * deltaTime);
        }

        public void ChangeHealth(float amount)
        {
            currentHealth += amount;

            switch (amount) {
                case > 0:
                    Healed.Invoke(amount);
                    break;
                case < 0:
                    Damaged.Invoke(-amount);
                    break;
                default:
                    return;
            }

            if (currentHealth <= 0) {
                currentHealth = 0;
                HealthReachedMin.Invoke();
                return;
            }

            if (currentHealth >= maxHealth) {
                currentHealth = maxHealth;
                HealthReachedMax.Invoke();
            }
        }

        public void AddHealthChanger(float changeAmount)
        {
            currentHealthChanger += changeAmount;
            HealthChangerChanged.Invoke(changeAmount);
        }

        public void Reset()
        {
            currentHealth = maxHealth;
            currentHealthChanger = initialHealthChanger;
        }
    }
}