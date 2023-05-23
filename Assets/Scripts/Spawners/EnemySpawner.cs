using Entities;
using UnityEngine;

namespace Spawners
{
    public class EnemySpawner : Spawner<Enemy>, IAgeChangeable
    {
        public void ChangeAge(AgeType age)
        {
            foreach (var entity in spawnedEntities) {
                entity.ChangeAge(age);
            }
        }
    }
}