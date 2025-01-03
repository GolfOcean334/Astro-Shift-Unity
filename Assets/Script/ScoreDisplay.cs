using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI lastScoreText;

    private void Start()
    {
        if (ScoreManager.Instance != null)
        {
            if (highScoreText != null)
            {
                highScoreText.text = "High Score: " + ScoreManager.Instance.BestScore.ToString();
            }

            if (lastScoreText != null)
            {
                lastScoreText.text = "Last Score: " + ScoreManager.Instance.LastScore.ToString();
            }
        }
    }
}
