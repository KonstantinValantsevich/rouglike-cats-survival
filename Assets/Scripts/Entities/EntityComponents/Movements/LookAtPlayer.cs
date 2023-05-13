using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class LookAtPlayer : FollowPlayerMovement
    {
        public LookAtPlayer(float movementSpeed, Transform movementTransform, Transform rotationTransform,
            IPlayerState player) : base(movementSpeed, movementTransform, rotationTransform, player)
        {
        }

        public override void Tick(float deltaTime)
        {
            SetLookRotation(player.Position);
        }
    }
}