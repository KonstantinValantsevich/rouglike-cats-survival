using System;
using System.Collections.Generic;
using Entities.Collectibles;
using UnityEngine;

namespace Entities.EntityComponents
{
    public class Inventory
    {
        private int level;
        public int experienceLeftToNextLevel;
        public HashSet<ArtefactType> artefacts = new HashSet<ArtefactType>();
        public event Action<ArtefactType> ArtefactCollected = delegate { };
        public event Action LevelIncreased = delegate { };
        public event Action OnExperienceRecieved = delegate { };

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
            if (artefacts.Contains(artefactType)) {
                return;
            }
            artefacts.Add(artefactType);
            ArtefactCollected.Invoke(artefactType);
            Debug.Log($"Artefact collected:{artefactType}");
        }

        public void AddExperience(int experienceAmount)
        {
            experienceLeftToNextLevel -= experienceAmount;

            while (experienceLeftToNextLevel <= 0) {
                LevelIncreased.Invoke();
                level++;
                experienceLeftToNextLevel += GetExperienceToNextLevel(level);
                Debug.Log($"Level increased: {level}");
            }
            OnExperienceRecieved.Invoke();
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

        public int GetExperienceToNextLevel(int currentLevel)
        {
            return Mathf.RoundToInt(20f * currentLevel + 10f);
        }
    }
}