using System.Collections.Generic;
using Entities;
using Spawners;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Application : MonoBehaviour
{
    public Player player;
    public Boss boss;
    public Timer timer;
    public EnemySpawner enemySpawner;

    public List<GameObject> ageChangeableListGO;
    private List<IAgeChangeable> ageChangeableList;

    private AgeType currentAge = AgeType.Ancient;

    private void Start()
    {
        ageChangeableList = new List<IAgeChangeable>();
        foreach (var go in ageChangeableListGO) {
            if (go.TryGetComponent<IAgeChangeable>(out var ageChangeable)) {
                ageChangeableList.Add(ageChangeable);
            }
        }

        player.Initialise(player);

        player.EntityKilled += OnPlayerKilled;
        timer.StartTimer();
        timer.MajorMarkElapsed += SpawnBoss;
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
        b.EntityKilled += BossDefeated;
    }

    private void BossDefeated(Entity boss)
    {
        if (currentAge == AgeType.Future) {
            Debug.Log("You Won!");
        }
        currentAge++;
        foreach (var ageChangeable in ageChangeableList) {
            ageChangeable.ChangeAge(currentAge);
        }
        enemySpawner.enabled = true;
    }
}