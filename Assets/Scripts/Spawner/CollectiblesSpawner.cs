using System.Collections.Generic;
using Entities;
using Entities.Collectibles;
using Entities.Interfaces;
using UnityEngine;

namespace Spawner
{
    public class CollectiblesSpawner : MonoBehaviour
    {
        private IPlayerState player;

        public List<Collectible> collectiblesPrefabsList;

        private void Start()
        {
            player = FindObjectOfType<Player>();
        }

        private void Update()
        {
            SpawnCollectible();
        }

        private void SpawnCollectible()
        {
            Debug.Log($"{player.cameraRect.yMin} | {player.cameraRect.xMax}");
        }
    }
}