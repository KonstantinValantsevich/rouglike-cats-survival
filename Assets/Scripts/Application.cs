using Entities;
using UI;
using UnityEngine;

public class Application : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        player.Initialise(player);
    }
}