using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float laserSpeed;
    public GameObject hitParticle;
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
        GameObject effect = Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(effect, 0.1f);
        Destroy(gameObject);
    }
}
