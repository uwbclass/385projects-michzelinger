using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float laserSpeed;
    void Start()
    {
        Destroy(gameObject, 5);
       // laserSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * (laserSpeed * Time.smoothDeltaTime);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D  collider)
    { 
        Destroy(gameObject);
    }
}
