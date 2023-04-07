
using Player;

public class Enemy : Entity
{
    private IPlayerState player; // TODO: Change to interface with position

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        var playerPosition = player.Position;
        SetLookRotation(playerPosition);
        Move(transform.right);
    }
}