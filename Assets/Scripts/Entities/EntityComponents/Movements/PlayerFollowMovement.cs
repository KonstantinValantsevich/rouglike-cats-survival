using Entities.Interfaces;
using UnityEngine;

namespace Entities.EntityComponents.Movements
{
    public class PlayerFollowMovement : Movement
    {
        private IPlayerState player;

        public PlayerFollowMovement(float movementSpeed, Transform transform, IPlayerState player) : base(movementSpeed,
            transform)
        {
            this.player = player;
        }

        public override void Tick(float deltaTime)
        {
            SetLookRotation(player.Position);
            Move(transform.right);
        }
    }
}