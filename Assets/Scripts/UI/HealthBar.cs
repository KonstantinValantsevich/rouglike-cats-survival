using Entities.EntityComponents.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        public Image healthBarImage;

        private IHealth trackingHealth;

        public void Initialise(IHealth health)
        {
            trackingHealth = health;

            trackingHealth.Damaged += UpdateHealthBar;
            trackingHealth.Healed += UpdateHealthBar;
        }

        private void UpdateHealthBar(float amount)
        {
            healthBarImage.fillAmount = trackingHealth.CurrentHealth / trackingHealth.MaxHealth;
        }
    }
}