using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private float spawnRangeX = 50f;
    [SerializeField] private float spawnRangeZ = 50f;
    [SerializeField] private float spawnRangeY = 20f;
    [SerializeField] private int maxPlanets = 10;
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;

    private List<GameObject> activePlanets = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxPlanets; i++)
        {
            SpawnPlanet(true);
        }
    }

    void Update()
    {
        for (int i = activePlanets.Count - 1; i >= 0; i--)
        {
            GameObject planet = activePlanets[i];
            planet.transform.Translate(Vector3.back * planet.GetComponent<Planet>().speed * Time.deltaTime);

            if (planet.transform.position.z < -10f)
            {
                Destroy(planet);
                activePlanets.RemoveAt(i);
                SpawnPlanet(false);
            }
        }
    }

    void SpawnPlanet(bool initialSpawn)
    {
        GameObject prefab = planetPrefabs[Random.Range(0, planetPrefabs.Length)];

        Vector3 spawnPosition;
        float spawnY = GenerateRandomY();
        if (initialSpawn)
        {
            spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnY, Random.Range(-spawnRangeZ, spawnRangeZ));
        }
        else
        {
            spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnY, spawnRangeZ);
        }

        GameObject newPlanet = Instantiate(prefab, spawnPosition, Quaternion.identity);

        float randomSpeed = Random.Range(minSpeed, maxSpeed);
        newPlanet.AddComponent<Planet>().speed = randomSpeed;

        activePlanets.Add(newPlanet);
    }

    float GenerateRandomY()
    {
        float y;
        do
        {
            y = Random.Range(-spawnRangeY, spawnRangeY);
        } while (y > -6f && y < 8f);
        return y;
    }
}
