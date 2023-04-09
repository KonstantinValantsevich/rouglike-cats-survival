using System.Collections.Generic;
using Player;
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
        public int spawnRingRadius = 4;

        private IPlayerState player;

        private HashSet<Enemy> instantiatedEnemies;

        private float lastEnemyInstantiated = 0;

        private void Start()
        {
            player = FindObjectOfType<PlayerMain>();
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
            enemy.OnDead += OnEnemyDead;

            instantiatedEnemies.Add(enemy);
        }

        private Vector3 ChooseSpawnPosition()
        {
            return Random.insideUnitCircle + new Vector2(0.3f, 0.3f) * spawnRingRadius;
        }

        private void OnEnemyDead(Entity entity)
        {
            entity.OnDead -= OnEnemyDead;
            instantiatedEnemies.Remove((Enemy) entity);
        }
    }
}