using System.Collections.Generic;
using Entities;
using Entities.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Entity
    {
        public T entityPrefab;
        public Transform entitiesRoot;


        public int maxEntities = 5;

        public float spawnCooldownTime = 0.5f;
        public float spawnRingRadius = 4;

        protected IPlayerState player;

        private float timeLastEntitySpawned = 0;

        private HashSet<T> spawnedEntities;

        protected virtual void Start()
        {
            player = FindObjectOfType<Player>();
            spawnedEntities = new HashSet<T>(maxEntities);
        }

        private void Update()
        {
            if (CanBeSpawned())
            {
                SpawnEntity();
            }
        }

        protected virtual T SpawnEntity()
        {
            var entity = Instantiate(entityPrefab, RandomPositionBehindCamera(), Quaternion.identity,
                entitiesRoot);
            InitialiseEntity(entity);
            entity.Destroyed += EnemyDestroyed;

            spawnedEntities.Add(entity);
            return entity;
        }

        protected virtual void InitialiseEntity(T entity)
        {
        }

        protected Vector3 RandomPositionBehindCamera()
        {
            var spawnPosition = Vector3.zero;
            var cameraRect = player.CameraRect;
            var prohibitedCircleRadius = Mathf.Max(cameraRect.width, cameraRect.height);
            for (var i = 0; i < 10; i++)
            {
                spawnPosition = Random.insideUnitCircle * (prohibitedCircleRadius + spawnRingRadius);
                if (!cameraRect.Contains(spawnPosition))
                {
                    break;
                }
            }

            return spawnPosition;
        }

        protected virtual bool CanBeSpawned()
        {
            if (Time.time - timeLastEntitySpawned < spawnCooldownTime)
            {
                return false;
            }

            if (spawnedEntities.Count >= maxEntities)
            {
                return false;
            }

            timeLastEntitySpawned = Time.time;

            return true;
        }

        private void EnemyDestroyed(Entity entity)
        {
            entity.Destroyed -= EnemyDestroyed;
            spawnedEntities.Remove((T) entity);
        }
    }
}