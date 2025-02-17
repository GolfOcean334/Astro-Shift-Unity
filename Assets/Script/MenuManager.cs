using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool hasShownTutorial = false;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] playAudioClip;
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

    public void PlaySound(AudioSource audioSource)
    {
        if (playAudioClip.Length > 0)
        {
            int randomIndex = Random.Range(0, playAudioClip.Length);
            audioSource.PlayOneShot(playAudioClip[randomIndex]);
        }
    }
}
