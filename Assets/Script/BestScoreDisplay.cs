using TMPro;
using UnityEngine;

public class BestScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;

    private void Start()
    {
        if (ScoreManager.Instance != null && highScoreText != null)
        {
            highScoreText.text = "High Score: " + ScoreManager.Instance.BestScore.ToString();
        }
    }
}