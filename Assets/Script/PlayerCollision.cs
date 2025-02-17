using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerCollision : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image fadeImage;

    bool isInvulnerable = false;
    [SerializeField] private float invulnerabilityDuration = 0.1f;

    [SerializeField] private AudioSource hitSource;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioSource loseSource;
    [SerializeField] private AudioClip[] loseSounds;

    public static bool isGameOver = false;
    void Start()
    {
        currentLives = maxLives;
        UpdateHealthBar();
        isGameOver = false;
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

        if (currentLives <= 0)
        {
            GameOver();
        }

        UpdateHealthBar();
        StartCoroutine(InvulnerabilityRoutine());
    }

    void UpdateHealthBar()
    {
        float healthFraction = (float)currentLives / maxLives;
        healthBar.DOFillAmount(healthFraction, 0.3f).SetEase(Ease.OutQuad);
    }

    void GameOver()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.EndGame();
        }
        isGameOver = true;

        StartCoroutine(PlayLoseSoundAndFadeToBlack());
    }

    System.Collections.IEnumerator PlayLoseSoundAndFadeToBlack()
    {
        float clipLength = PlayRandomLoseSound();
        StartCoroutine(SlowTimeScale(clipLength));

        fadeImage.gameObject.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.DOFade(1, clipLength).SetEase(Ease.InQuad).SetUpdate(true);

        yield return new WaitForSecondsRealtime(clipLength);
        GoMenu();
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
        Cursor.lockState = CursorLockMode.None;
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
