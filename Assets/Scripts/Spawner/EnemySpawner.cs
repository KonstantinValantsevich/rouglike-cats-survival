using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        public int maxEnemies = 5;
        public Enemy enemyPrefab;
        public Transform enemiesRoot;

        public int spawnRingRadius = 4;

        private IPlayerState player;

        private HashSet<Enemy> instantiatedEnemies;

        private void Start()
        {
            player = FindObjectOfType<PlayerMain>();
            instantiatedEnemies = new HashSet<Enemy>(maxEnemies);
            StartCoroutine(InstantiateEnemiesCoroutine());
        }

        private IEnumerator InstantiateEnemiesCoroutine()
        {
            while (true)
            {
                var enemy = Instantiate(enemyPrefab, ChooseSpawnPosition(), Quaternion.identity, enemiesRoot);
                enemy.Init(player);
                enemy.OnDead += OnEnemyDead;

                instantiatedEnemies.Add(enemy);
                yield return new WaitForSeconds(1);
                yield return new WaitUntil(() => instantiatedEnemies.Count < maxEnemies);
            }
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