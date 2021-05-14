using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= 10; i++)
        {
            Instantiate(asteroidPrefab, new Vector3(Random.Range(0, 10), Random.Range(0,10), 0), Quaternion.identity);
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
