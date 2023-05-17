using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textTimer;
    private TimeSpan time = new TimeSpan(0);
    private bool isRunning;

    public event Action MajorMarkElapsed = () => { };

    public void StartTimer()
    {
        isRunning = true;
    }

    private void Update()
    {
        if (!isRunning) {
            return;
        }
        time += new TimeSpan(0, 0, 0, 0, (int) (Time.deltaTime * 1000));

        textTimer.text = time.ToString(@"mm\:ss");
        if (time.TotalMinutes % 5 == 0) {
            MajorMarkElapsed.Invoke();
        }
    }

    public void PauseTimer()
    {
        isRunning = false;
    }
}