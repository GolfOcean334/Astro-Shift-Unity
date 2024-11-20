using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    [SerializeField] private Image healthBar;

    [SerializeField] private float invulnerabilityDuration = 0.01f;

    void Start()
    {
        currentLives = maxLives;
        UpdateHealthBar();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Asteroid"))
        {
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
        Debug.Log("Game Over!");
        Time.timeScale = 0;
    }

    System.Collections.IEnumerator InvulnerabilityRoutine()
    {
        yield return new WaitForSeconds(invulnerabilityDuration);
    }
}
