using System;
using DG.Tweening;
using Entities.EntityComponents;
using Entities.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.Collectibles
{
    public enum ArtefactType
    {
        Random,
        AirStrikeI,
        AirStrikeII,
        AutoI,
        AutoII,
        FireAoEI,
        FireAoEII,
        FireAoEDebuffI,
        FireAoEDebuffII,
        ForwardI,
        ForwardII,
        FlameI,
        FlameII,
        MegaI,
        MegaII
    }

    public class Artefact : Collectible
    {
        [Header("Artefact")]
        public ArtefactType type;

        public ParticleSystem vfx;

        public override void Initialise(IPlayerState player)
        {
            Player = player;

            InitialiseComponents();

            UpdateTickables();

            Health.Damaged += amount => Debug.Log($"Entity: {name} was damaged by {amount} damage");
            if (type != ArtefactType.Random) {
                return;
            }

            var range = Enum.GetValues(typeof(ArtefactType)).Length;
            type = (ArtefactType) Random.Range(1, range);
        }

        public override void CollectItem(Inventory inventory)
        {
            inventory.CollectArtefact(type);
            
            transform.SetParent(Player.Transform);
            transform.DOLocalJump(Random.insideUnitCircle * 5, Random.value, 1, 1).PrependCallback(() => {
                    vfx.time = 0;
                    vfx.gameObject.SetActive(true);
                })
                .Append(transform.DOLocalMove(Vector3.zero, 0.3f))
                .AppendCallback(() => {
                    vfx.gameObject.SetActive(false);
                    KillEntity();
                });
        }
    }
}