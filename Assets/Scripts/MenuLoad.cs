using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoad : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}