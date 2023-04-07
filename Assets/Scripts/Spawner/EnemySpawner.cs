using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        public int maxEnemies = 5;
        public Enemy enemiePrefab;
        public Transform enemiesRoot;

        public IPlayerState player;
        
        private HashSet<Enemy> instantiatedEnemies;

        private void Start()
        {
            player = FindObjectOfType<PlayerMovement>();
            instantiatedEnemies = new HashSet<Enemy>(maxEnemies);
            StartCoroutine(InstantiateEnemiesCoroutine());
        }

        private IEnumerator InstantiateEnemiesCoroutine()
        {
            while (true) {
                var enemy = Instantiate(enemiePrefab, enemiesRoot);
                instantiatedEnemies.Add(enemy);
                yield return new WaitForSeconds(1);
                yield return new WaitUntil(() => instantiatedEnemies.Count < maxEnemies);
            }
        }
    }
}