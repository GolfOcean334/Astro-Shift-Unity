using TMPro;
using UnityEngine;

public class CurrentScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText;

    void Update()
    {
        UpdateCurrentScoreDisplay();
    }

    private void UpdateCurrentScoreDisplay()
    {
        if (currentScoreText != null && ScoreManager.Instance != null)
        {
            currentScoreText.text = Mathf.FloorToInt(ScoreManager.Instance.CurrentScore).ToString();
        }
    }
}
