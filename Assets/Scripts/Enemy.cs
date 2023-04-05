public class Enemy : Entity
{
    private Player player; // TODO: Change to interface with position

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        var playerPosition = player.transform.position;
        SetLookRotation(playerPosition);
        Move(transform.right);
    }
}