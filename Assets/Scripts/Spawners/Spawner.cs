using System.Collections.Generic;
using Entities;
using Entities.Interfaces;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Entity, IAgeChangeable
    {
        public T entityPrefab;
        public Transform entitiesRoot;

        private ObjectPool<T> objectPool;
        protected List<T> spawnedEntities;

        public int maxEntities = 5;

        public float spawnCooldownTime = 0.5f;
        public float spawnRingRadius = 4;

        protected IPlayerState player;

        private float timeLastEntitySpawned = 0;

        protected virtual void Start()
        {
            if (maxEntities <= 0) {
                return;
            }

            spawnedEntities = new List<T>(maxEntities);
            objectPool = new ObjectPool<T>(createFunc: SpawnEntity, actionOnGet: ActivateEntity,
                actionOnRelease: DeactivateEntity, defaultCapacity: maxEntities, maxSize: maxEntities);
            player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            if (CanBeSpawned()) {
                objectPool.Get();
            }
        }

        protected virtual T SpawnEntity()
        {
            var entity = Instantiate(entityPrefab, Vector3.zero, Quaternion.identity,
                entitiesRoot);
            entity.Initialise(player);

            InitialiseEntity(entity);

            entity.EntityKilled += ent => objectPool.Release((T) ent);

            return entity;
        }

        protected virtual void ActivateEntity(T entity)
        {
            entity.Activate();
            InitialiseEntity(entity);
        }

        protected virtual void DeactivateEntity(T entity)
        {
            entity.Deactivate();
        }

        protected virtual void InitialiseEntity(T entity)
        {
            entity.transform.position = RandomPositionBehindCamera();
        }

        protected Vector3 RandomPositionBehindCamera()
        {
            var spawnPosition = Vector3.zero;
            var cameraRect = player.CameraRect;
            var prohibitedCircleRadius = player.CameraRectCircleRadius;
            while (true) {
                spawnPosition = cameraRect.center +
                                Random.insideUnitCircle * (prohibitedCircleRadius + spawnRingRadius);
                if (!cameraRect.Contains(spawnPosition)) {
                    break;
                }
            }

            return spawnPosition;
        }

        protected virtual bool CanBeSpawned()
        {
            if (maxEntities <= 0) {
                return false;
            }

            if (Time.time - timeLastEntitySpawned < spawnCooldownTime) {
                return false;
            }

            if (objectPool.CountActive >= maxEntities) {
                return false;
            }

            timeLastEntitySpawned = Time.time;

            return true;
        }

        public void KillAll()
        {
            var ent = entitiesRoot.transform.GetComponentsInChildren<T>();
            foreach (var entity in ent) {
                entity.Health.ChangeHealth(-float.MaxValue);
            }
        }
        
        public void ChangeAge(AgeType age)
        {
            foreach (var entity in spawnedEntities) {
                entity.ChangeAge(age);
            }
        }
    }
}