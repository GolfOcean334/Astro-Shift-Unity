using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCollision : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    [SerializeField] private Image healthBar;

    bool isInvulnerable = false;
    [SerializeField] private float invulnerabilityDuration = 0.1f;

    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioSource loseSource;
    [SerializeField] private AudioClip[] loseSounds;

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
            PlayRandomMoveSound();
            Destroy(other.gameObject);
        }
    }

    void LoseLife()
    {
        currentLives--;

        UpdateHealthBar();
        StartCoroutine(InvulnerabilityRoutine());
    }

    void UpdateHealthBar()
    {
        float healthFraction = (float)currentLives / maxLives;
        healthBar.DOFillAmount(healthFraction, 0.3f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            if (currentLives <= 0)
            {
                GameOver();
            }
        });
    }

    void GameOver()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.EndGame();
        }
        StartCoroutine(PlayLoseSoundAndGoToMenu());
    }

    System.Collections.IEnumerator PlayLoseSoundAndGoToMenu()
    {
        float clipLength = PlayRandomLoseSound();
        StartCoroutine(SlowTimeScale(clipLength));
        yield return new WaitForSecondsRealtime(clipLength);
        Debug.Log("Game Over!");
        GoMenu();
        Cursor.lockState = CursorLockMode.None;
    }

    System.Collections.IEnumerator SlowTimeScale(float duration)
    {
        float startTime = Time.unscaledTime;
        while (Time.unscaledTime < startTime + duration)
        {
            Time.timeScale = Mathf.Lerp(1, 0, (Time.unscaledTime - startTime) / duration);
            yield return null;
        }
        Time.timeScale = 0;
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

    void PlayRandomMoveSound()
    {
        if (hitSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            hitSource.PlayOneShot(hitSounds[randomIndex]);
        }
    }

    float PlayRandomLoseSound()
    {
        if (loseSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, loseSounds.Length);
            AudioClip clipToPlay = loseSounds[randomIndex];
            loseSource.PlayOneShot(clipToPlay);
            return clipToPlay.length;
        }
        return 0f;
    }
}
