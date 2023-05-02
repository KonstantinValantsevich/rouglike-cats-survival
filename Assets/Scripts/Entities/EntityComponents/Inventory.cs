using System;
using System.Collections.Generic;
using Entities.Collectibles;
using UnityEngine;

namespace Entities.EntityComponents
{
    public class Inventory
    {
        private int level;
        private int experienceLeftToNextLevel;
        public HashSet<ArtefactType> artefacts = new HashSet<ArtefactType>();
        public event Action<ArtefactType> artefactCollected = delegate { };
        public event Action levelIncreased = delegate { };

        public Inventory(int level)
        {
            Level = level;
        }

        public int Level {
            get => level;
            private set {
                level = value;
                experienceLeftToNextLevel = GetExperienceToNextLevel(level);
            }
        }

        public void CollectArtefact(ArtefactType artefactType)
        {
            artefacts.Add(artefactType);
            artefactCollected.Invoke(artefactType);
            Debug.Log($"Artefact collected:{artefactType}");
        }

        public void AddExperience(int experienceAmount)
        {
            experienceLeftToNextLevel -= experienceAmount;

            while (experienceLeftToNextLevel <= 0) {
                levelIncreased.Invoke();
                level++;
                experienceLeftToNextLevel += GetExperienceToNextLevel(level);
                Debug.Log($"Level increased: {level}");
            }
        }

        public void SubtractExperience(int experienceAmount)
        {
            experienceLeftToNextLevel += experienceAmount;

            var experienceToNextLevel = GetExperienceToNextLevel(level);

            if (experienceLeftToNextLevel >= experienceToNextLevel) {
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