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

    void Start()
    {
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
    }
}
