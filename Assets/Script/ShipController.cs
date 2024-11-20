using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float columnSpacing = 2.0f; // Espace entre les couloirs
    private int currentLane = 2; // Position initiale (couloir 3 sur 5)

    private bool isDodging = false;
    private float dodgeCooldownTimer = 0f;

    [SerializeField] private float dodgeDuration = 0.5f; // Durée d'invulnérabilité
    [SerializeField] private float dodgeCooldown = 10f; // Temps de recharge de l'esquive

    Collider shipCollider;

    [SerializeField] private Image imgDodge;
    [SerializeField] private Color dodgeUnavailableColor = new Color(1, 1, 1, 0.5f); // Couleur grisée
    [SerializeField] private Color dodgeAvailableColor = new Color(1, 1, 1, 1); // Couleur normale

    private PlayerControls controls;

    private Animator animator;

    void Awake()
    {
        controls = new PlayerControls();
        shipCollider = GetComponent<Collider>();
        shipCollider.enabled = true;

        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Dodge.performed += ctx => Dodge();

        animator = GetComponent<Animator>();
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
        }
        else if (direction.x > 0 && currentLane < 4) // Droite
        {
            animator.SetTrigger("MoveRight");
            currentLane++;
            UpdatePosition();
        }
    }

    void Dodge()
    {
        if (!isDodging && dodgeCooldownTimer <= 0f)
        {
            Debug.Log("Dodge");
            shipCollider.enabled = false;
            StartCoroutine(DodgeRoutine());
        }
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(currentLane * columnSpacing, transform.position.y, transform.position.z);
    }

    System.Collections.IEnumerator DodgeRoutine()
    {
        isDodging = true;
        dodgeCooldownTimer = dodgeCooldown;

        yield return new WaitForSeconds(dodgeDuration);
        shipCollider.enabled = true;
        isDodging = false;
    }

    void UpdateDodgeUI()
    {
        if (dodgeCooldownTimer > 0f)
        {
            imgDodge.color = dodgeUnavailableColor;
            imgDodge.fillAmount = 1 - (dodgeCooldownTimer / dodgeCooldown);
        }
        else
        {imgDodge.color = dodgeAvailableColor;
            imgDodge.fillAmount = 1;
        }
    }
}
