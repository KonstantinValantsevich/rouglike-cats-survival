using Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Application : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player.Initialise(player);

        player.EntityKilled += OnPlayerKilled;
    }

    private void OnPlayerKilled(Entity player)
    {
        SceneManager.LoadScene("MainScene");
    }
}