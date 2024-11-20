using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public Transform[] lanes;
    [SerializeField] private float minSpawnInterval = 1.0f;
    [SerializeField] private float maxSpawnInterval = 3.0f;
    [SerializeField] private int minAsteroidsPerWave = 2;
    [SerializeField] private int maxAsteroidsPerWave = 4;

    private float spawnTimer;

    void Start()
    {
        ScheduleNextWave();
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnWave();
            ScheduleNextWave();
        }
    }

    void ScheduleNextWave()
    {
        spawnTimer = Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnWave()
    {
        int asteroidCount = Random.Range(minAsteroidsPerWave, maxAsteroidsPerWave + 1);

        for (int i = 0; i < asteroidCount; i++)
        {
            int randomLane = Random.Range(0, lanes.Length);
            Instantiate(asteroidPrefab, lanes[randomLane].position, Quaternion.identity);
        }
    }
}
