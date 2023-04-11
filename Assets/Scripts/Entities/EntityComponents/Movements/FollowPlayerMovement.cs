using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class PlayerFollowMovement : Movement
    {
        private IPlayerState player;

        public PlayerFollowMovement(float movementSpeed, Transform movementTransform, Transform rotationTransform,
            IPlayerState player) : base(
            movementSpeed, movementTransform, rotationTransform)
        {
            this.player = player;
        }

        public override void Tick(float deltaTime)
        {
            SetLookRotation(player.Position);
            Move(movementTransform.right);
        }
    }
}