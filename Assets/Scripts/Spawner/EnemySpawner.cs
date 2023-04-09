using System.Collections.Generic;
using Entities;
using Entities.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        public int maxEnemies = 5;
        public Enemy enemyPrefab;
        public Transform enemiesRoot;
        public float spawnCooldownTime = 0.5f;
        public float spawnRingRadius = 4;
        public float spawnProhibitedRadius = 5;
        private IPlayerState player;

        private HashSet<Enemy> instantiatedEnemies;

        private float lastEnemyInstantiated = 0;

        private void Start()
        {
            player = FindObjectOfType<Player>();
            instantiatedEnemies = new HashSet<Enemy>(maxEnemies);
        }

        private void Update()
        {
            InstantiateEnemy();
        }

        private void InstantiateEnemy()
        {
            if (Time.time - lastEnemyInstantiated < spawnCooldownTime)
            {
                return;
            }

            if (instantiatedEnemies.Count >= maxEnemies)
            {
                return;
            }

            lastEnemyInstantiated = Time.time;
            var enemy = Instantiate(enemyPrefab, ChooseSpawnPosition(), Quaternion.identity, enemiesRoot);
            enemy.Init(player);
            enemy.Destroyed += EnemyDestroyed;

            instantiatedEnemies.Add(enemy);
        }

        private Vector3 ChooseSpawnPosition()
        {
            var prohibitedSpawnCoefficient = (spawnProhibitedRadius + spawnRingRadius) / spawnProhibitedRadius;
            var randomCirclePosition = Random.insideUnitCircle;
            var prohibitedPart = new Vector2(prohibitedSpawnCoefficient, prohibitedSpawnCoefficient) *
                                 randomCirclePosition.normalized;
            var spawnPositionOffset = (randomCirclePosition + prohibitedPart) * spawnRingRadius;
            return player.Position + new Vector3(spawnPositionOffset.x, spawnPositionOffset.y, 0);
        }

        private void EnemyDestroyed(Entity entity)
        {
            entity.Destroyed -= EnemyDestroyed;
            instantiatedEnemies.Remove((Enemy) entity);
        }
    }
}