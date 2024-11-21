using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    private float score;
    private float time;

    [SerializeField] private TextMeshProUGUI scoreText;

    void Start()
    {
        score = 0;
        time = 0;
        UpdateScoreText();
    }

    void Update()
    {
        GetTimeSpent();
        UpdateScore();
    }

    float GetTimeSpent()
    {
        time += Time.deltaTime;
        return time;
    }

    void UpdateScore()
    {
        score = GetTimeSpent() * 5;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }
}
