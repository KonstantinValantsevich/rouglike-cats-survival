using Player;
using Projectiles;
using UnityEngine;

public class Enemy : Entity
{
    private IPlayerState player;

    public void Init(IPlayerState player)
    {
        this.player = player;
    }

    private void Update()
    {
        SetLookRotation(player.Position);
        Move(transform.right);
    }
}