using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    int asteroidCount;
    public float maxVerticalSpread = 5.0f;
    public float leftEdge = -50.0f;
    public int maxAsteroids = 100;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= maxAsteroids; i++)
        {
            Instantiate(asteroidPrefab, transform.TransformPoint(Random.Range(leftEdge, 0.0f), Random.Range(-maxVerticalSpread, maxVerticalSpread), 0.0f), Quaternion.identity);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if(asteroidCount < maxAsteroids)
        {
            Instantiate(asteroidPrefab, transform.TransformPoint(0.0f, Random.Range(-maxVerticalSpread, maxVerticalSpread), 0.0f), Quaternion.identity);
        }
    }

    public void IncrementAsteroidCount()
    {
        asteroidCount++;
    }

    public void DecrementAsteroidCount()
    {
        asteroidCount--;
    }
}
