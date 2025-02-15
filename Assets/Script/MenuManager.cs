using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool hasShownTutorial = false;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
