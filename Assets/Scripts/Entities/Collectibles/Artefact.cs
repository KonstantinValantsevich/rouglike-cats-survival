using System;
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

        public override void Initialise(IPlayerState player)
        {
            base.Initialise(player);
            if (type != ArtefactType.Random) {
                return;
            }

            var range = Enum.GetValues(typeof(ArtefactType)).Length;
            type = (ArtefactType) Random.Range(1, range);
        }

        public override void CollectItem(Inventory inventory)
        {
            inventory.CollectArtefact(type);
        }
    }
}