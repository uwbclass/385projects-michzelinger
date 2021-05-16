using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    AsteroidController asteroidController;
    Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        asteroidController = FindObjectOfType<AsteroidController>();
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(Random.Range(-1.0f, -0.5f), 0.0f);
        rb2d.angularVelocity = Random.Range(-120.0f, 120.0f);
        float scale = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector3(scale, scale, 1);

        asteroidController.IncrementAsteroidCount();
    }

    void Update()
    {
        if(transform.position.x <= -15)
        {
            asteroidController.DecrementAsteroidCount();
            Destroy(gameObject);
        }
    }

    
}
