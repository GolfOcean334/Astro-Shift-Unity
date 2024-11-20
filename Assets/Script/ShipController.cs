using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public float columnSpacing = 2.0f; // Espace entre les couloirs
    private int currentLane = 2; // Position initiale (couloir 3 / couloir milieux)

    private bool isDodging = false;
    private float dodgeCooldownTimer = 0f;

    public float dodgeDuration = 0.5f; // Durée d'invulnérabilité
    public float dodgeCooldown = 10f; // Temps de recharge de l'esquive

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
        controls.Player.Dodge.performed += ctx => Dodge();
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
    }

    void Move(Vector2 direction)
    {
        if (direction.x < 0 && currentLane > 0) // Gauche
        {
            currentLane--;
            UpdatePosition();
        }
        else if (direction.x > 0 && currentLane < 4) // Droite
        {
            currentLane++;
            UpdatePosition();
        }
    }

    void Dodge()
    {
        if (!isDodging && dodgeCooldownTimer <= 0f)
        {
            Debug.Log("Dodge");
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

        isDodging = false;
    }
}
