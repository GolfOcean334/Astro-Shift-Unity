using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 5.0f;
    [SerializeField] private float maxSpeed = 2000.0f;
    [SerializeField] private float accelerationRate = 0.5f;
    [SerializeField] private float destroyZ = -10.0f;

    private float currentSpeed;
    private static float globalSpeedMultiplier = 1.0f;

    void Start()
    {
        currentSpeed = initialSpeed * globalSpeedMultiplier;
    }

    void Update()
    {
        if (globalSpeedMultiplier < maxSpeed / initialSpeed)
        {
            globalSpeedMultiplier += accelerationRate * Time.deltaTime;
        }

        currentSpeed = initialSpeed * globalSpeedMultiplier;
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);

        if (transform.position.z <= destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
