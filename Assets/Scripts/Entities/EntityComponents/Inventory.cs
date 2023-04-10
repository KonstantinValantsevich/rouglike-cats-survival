using UnityEngine;

namespace Entities.EntityComponents
{
    public class Inventory
    {
        private int level;
        private int experienceLeftToNextLevel;

        public Inventory(int level)
        {
            Level = level;
        }

        public int Level
        {
            get => level;
            set
            {
                level = value;
                experienceLeftToNextLevel = GetExperienceToNextLevel(level);
            }
        }

        public void AddExperience(int experienceAmount)
        {
            experienceLeftToNextLevel -= experienceAmount;

            if (experienceLeftToNextLevel <= 0)
            {
                level++;
                experienceLeftToNextLevel += GetExperienceToNextLevel(level);
                Debug.Log($"Level increased: {level}");
            }
        }

        public void SubtractExperience(int experienceAmount)
        {
            experienceLeftToNextLevel += experienceAmount;

            var experienceToNextLevel = GetExperienceToNextLevel(level);

            if (experienceLeftToNextLevel >= experienceToNextLevel)
            {
                level--;
                experienceLeftToNextLevel -= experienceToNextLevel;
            }
        }

        private int GetTotalExperienceInLevel(int level)
        {
            return Mathf.RoundToInt(level * (1.75f * level + 5));
        }

        private int GetExperienceToNextLevel(int currentLevel)
        {
            return Mathf.RoundToInt(3.5f * currentLevel + 3.25f);
        }
    }
}