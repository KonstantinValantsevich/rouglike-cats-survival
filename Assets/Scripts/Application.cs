using Entities;
using Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Application : MonoBehaviour
{
    public Player player;
    public Boss boss;

    public EnemySpawner enemySpawner;
    
    private void Start()
    {
        player.Initialise(player);

        player.EntityKilled += OnPlayerKilled;
    }

    private void OnPlayerKilled(Entity player)
    {
        SceneManager.LoadScene("MainScene");
    }

    [ContextMenu("Spawn Boss")]
    public void SpawnBoss()
    {
        enemySpawner.enabled = false;
        enemySpawner.KillAll();
        var b = Instantiate(boss);
        b.Initialise(player);
        b.transform.position = player.Position + Vector3.left * 10;
    }
}