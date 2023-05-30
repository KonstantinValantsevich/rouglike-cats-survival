using Entities.EntityComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ExpBar : MonoBehaviour
    {
        public Image barImage;
        public TextMeshProUGUI level;

        private Inventory inventory;

        public void Initialise(Inventory inventory)
        {
            this.inventory = inventory;
            inventory.OnExperienceRecieved += UpdateInfo;
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            barImage.fillAmount = 1 - (float) inventory.experienceLeftToNextLevel /
                inventory.GetExperienceToNextLevel(inventory.Level);
            level.text = $"Level: {inventory.Level}";
            //            healthBarImage.fillAmount = trackingHealth.CurrentHealth / trackingHealth.MaxHealth;
        }
    }
}