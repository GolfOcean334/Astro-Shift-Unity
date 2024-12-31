using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed;

    void Start()
    {
        speed = Random.Range(5f, 15f);

        float randomSize = Random.Range(1f, 5f);
        transform.localScale = Vector3.one * randomSize;
    }
}
