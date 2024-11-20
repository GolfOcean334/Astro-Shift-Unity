using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float initialSpeed = 5.0f;
    public float maxSpeed = 2000.0f;
    public float accelerationRate = 0.5f;
    public float destroyZ = -10.0f;

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
