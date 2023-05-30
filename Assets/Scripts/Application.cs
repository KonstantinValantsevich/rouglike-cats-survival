using System.Collections.Generic;
using Cinemachine;
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
    public CinemachineVirtualCamera Camera;

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
        SceneManager.LoadScene("Main Menu");
    }

    [ContextMenu("Spawn Boss")]
    public void SpawnBoss()
    {
        enemySpawner.enabled = false;
        enemySpawner.KillAll();
        timer.PauseTimer();
        var b = Instantiate(boss);
        Camera.m_Lens.OrthographicSize *= 1.5f;
        b.ChangeAge(currentAge);
        b.Initialise(player);
        var rPos = (Random.insideUnitCircle + Vector2.one) * 10;
        b.transform.position = player.Position + new Vector3(rPos.x, rPos.y, 0);
        b.EntityKilled += BossDefeated;
    }

    private void BossDefeated(Entity boss)
    {
        if (currentAge == AgeType.Future) {
            SceneManager.LoadScene("Main Menu");
            return;
        }
        currentAge++;
        timer.StartTimer();
        Camera.m_Lens.OrthographicSize /= 1.5f;
        foreach (var ageChangeable in ageChangeableList) {
            ageChangeable.ChangeAge(currentAge);
        }
        enemySpawner.enabled = true;
    }
}