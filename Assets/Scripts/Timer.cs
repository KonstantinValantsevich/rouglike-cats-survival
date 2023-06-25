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
        time = new TimeSpan(0, 2, 0);
    }

    private void Update()
    {
        if (!isRunning) {
            return;
        }
        time -= new TimeSpan(0, 0, 0, 0, (int) (Time.deltaTime * 1000));

        textTimer.text = $"Till boss spawn: {time:mm\\:ss}";
        if (time.TotalSeconds <= 0) {
            MajorMarkElapsed.Invoke();
        }
    }

    public void PauseTimer()
    {
        isRunning = false;
    }

    public void ContinueTimer()
    {
        isRunning = false;
    }
}