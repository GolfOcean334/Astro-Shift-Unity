using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float columnSpacing = 2.0f; // Espace entre les couloirs
    private int currentLane = 2; // Position initiale (couloir 3 sur 5)

    private bool isDodging = false;
    private bool isAnimatingDodge = false;
    private float dodgeCooldownTimer = 0f;

    [SerializeField] private float dodgeDuration = 0.5f; // Durée d'invulnérabilité
    [SerializeField] private float dodgeCooldown = 10f; // Temps de recharge de l'esquive

    private Collider shipCollider;

    [SerializeField] private Image imgDodge;
    [SerializeField] private Color dodgeUnavailableColor = new Color(1, 1, 1, 0.5f); // Couleur grisée
    [SerializeField] private Color dodgeAvailableColor = new Color(1, 1, 1, 1); // Couleur normale

    private PlayerControls controls;
    [SerializeField] private LaneManager laneManager;

    private Animator animator;

    [SerializeField] private GameObject shield;

    [SerializeField] private AudioSource moveSource;
    [SerializeField] private AudioClip[] moveSounds;
    [SerializeField] private AudioSource shieldSource;
    [SerializeField] private AudioClip[] shieldActivationSounds;

    void Awake()
    {
        controls = new PlayerControls();
        shipCollider = GetComponent<Collider>();
        shipCollider.enabled = true;

        shield.SetActive(false);

        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Dodge.performed += ctx => Dodge();

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        laneManager.HighlightLane(currentLane);
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    void Update()
    {
        dodgeCooldownTimer -= Time.deltaTime;
        UpdateDodgeUI();
    }

    void Move(Vector2 direction)
    {
        if (direction.x < 0 && currentLane > 0) // Gauche
        {
            animator.SetTrigger("MoveLeft");
            currentLane--;
            UpdatePosition();
            PlayRandomMoveSound();
        }
        else if (direction.x > 0 && currentLane < 4) // Droite
        {
            animator.SetTrigger("MoveRight");
            currentLane++;
            UpdatePosition();
            PlayRandomMoveSound();
        }
    }

    void Dodge()
    {
        if (!isDodging && dodgeCooldownTimer <= 0f)
        {
            shipCollider.enabled = false;
            StartCoroutine(DodgeRoutine());

            PlayRandomShieldActivationSound();
            AnimateDodgeIconOnActivated();
        }
    }


    void UpdatePosition()
    {
        transform.position = new Vector3(currentLane * columnSpacing, transform.position.y, transform.position.z);
        laneManager.HighlightLane(currentLane);
    }

    System.Collections.IEnumerator DodgeRoutine()
    {
        isDodging = true;
        dodgeCooldownTimer = dodgeCooldown;
        shield.SetActive(true);

        yield return new WaitForSeconds(dodgeDuration);
        shipCollider.enabled = true;
        isDodging = false;
        shield.SetActive(false);
    }

    void UpdateDodgeUI()
    {
        if (isAnimatingDodge) return;

        if (dodgeCooldownTimer > 0f)
        {
            imgDodge.color = dodgeUnavailableColor;
            imgDodge.fillAmount = 1 - (dodgeCooldownTimer / dodgeCooldown);
            imgDodge.transform.DOKill();
            imgDodge.transform.localScale = Vector3.one;
        }
        else
        {
            imgDodge.color = dodgeAvailableColor;
            imgDodge.fillAmount = 1;

            if (!DOTween.IsTweening(imgDodge.transform))
            {
                imgDodge.transform.DOScale(1.05f, dodgeDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            }
        }
    }

    void AnimateDodgeIconOnActivated()
    {
        isAnimatingDodge = true;

        imgDodge.transform.DOKill();
        imgDodge.transform.DOScale(1.3f, dodgeDuration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            imgDodge.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
            {
                isAnimatingDodge = false;
                imgDodge.transform.DOScale(1.05f, dodgeDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
            });
        });
    }

    void PlayRandomMoveSound() 
    {
        if (moveSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, moveSounds.Length);
            moveSource.Stop();
            moveSource.PlayOneShot(moveSounds[randomIndex]);
            moveSource.Play();
        }
    }
    void PlayRandomShieldActivationSound()
    {
        if (shieldActivationSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, shieldActivationSounds.Length);
            shieldSource.PlayOneShot(shieldActivationSounds[randomIndex]);
        }
    }
}
