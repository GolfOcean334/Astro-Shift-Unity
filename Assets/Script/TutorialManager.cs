using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage videoDisplay;
    [SerializeField] private Button nextButton;

    [SerializeField] private VideoClip[] tutorialVideos;
    private int currentVideoIndex = 0;

    private AsteroidSpawner asteroidSpawner;
    private ScoreManager scoreManager;
    private ShipController shipController;

    void Start()
    {
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
        if (asteroidSpawner != null)
        {
            asteroidSpawner.enabled = false;
        }
        scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.enabled = false;
        }
        shipController = FindObjectOfType<ShipController>();
        if (shipController != null)
        {
            shipController.enabled = false;
        }

        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.loopPointReached += OnVideoEnded;
        nextButton.onClick.AddListener(NextTutorial);
        ShowTutorial();
    }

    private void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        PlayVideo(currentVideoIndex);
    }

    private void PlayVideo(int index)
    {
        if (index < tutorialVideos.Length)
        {
            videoPlayer.clip = tutorialVideos[index];
            videoPlayer.Play();
        }
        else
        {
            CloseTutorial();
        }
    }

    private void OnVideoPrepared(VideoPlayer vp)
    {
        videoDisplay.texture = vp.targetTexture;

        float videoWidth = vp.width;
        float videoHeight = vp.height;
        float videoRatio = videoWidth / videoHeight;

        RectTransform rt = videoDisplay.GetComponent<RectTransform>();
        float screenRatio = (float)Screen.width / (float)Screen.height;

        if (videoRatio > screenRatio)
        {
            rt.sizeDelta = new Vector2(Screen.width, Screen.width / videoRatio);
        }
        else
        {
            rt.sizeDelta = new Vector2(Screen.height * videoRatio, Screen.height);
        }

        videoPlayer.Play();
    }

    private void OnVideoEnded(VideoPlayer vp)
    {
        vp.time = 0;
        vp.Play();
    }

    private void NextTutorial()
    {
        currentVideoIndex++;
        if (currentVideoIndex < tutorialVideos.Length)
        {
            PlayVideo(currentVideoIndex);
        }
        else
        {
            CloseTutorial();
        }
    }

    private void CloseTutorial()
    {
        tutorialPanel.SetActive(false);

        if (asteroidSpawner != null)
        {
            asteroidSpawner.enabled = true;
        }
        if (scoreManager != null)
        {
            scoreManager.enabled = true;
        }
        if (shipController != null)
        {
            shipController.enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
}
