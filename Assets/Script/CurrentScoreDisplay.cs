using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            currentScoreText.text = "Score: " + Mathf.FloorToInt(ScoreManager.Instance.CurrentScore).ToString();
        }
    }
}
