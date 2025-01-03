using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    [SerializeField] private Image healthBar;

    bool isInvulnerable = false;
    [SerializeField] private float invulnerabilityDuration = 0.1f;

    void Start()
    {
        currentLives = maxLives;
        UpdateHealthBar();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid") && isInvulnerable != true)
        {
            Debug.Log("Collision");
            LoseLife();
            Destroy(other.gameObject);
        }
    }

    void LoseLife()
    {
        currentLives--;

        UpdateHealthBar();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvulnerabilityRoutine());
        }
    }

    void UpdateHealthBar()
    {
        float healthFraction = (float)currentLives / maxLives;
        healthBar.fillAmount = healthFraction;
    }

    void GameOver()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.EndGame();
        }
        Debug.Log("Game Over!");
        Time.timeScale = 0;
        GoMenu();
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }
    System.Collections.IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }
}
