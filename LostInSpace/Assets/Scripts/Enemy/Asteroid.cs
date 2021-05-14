using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        rb2d.angularVelocity = Random.Range(-120, 120);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    
}
