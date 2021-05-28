using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public AudioClip laserFX;
    public float laserSpeed;
    public GameObject hitParticle;
    public int damageMultiplier;
    void Start()
    {
        AudioPlayer.instance.GetComponent<AudioSource>().PlayOneShot(laserFX);
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
        if(hitParticle != null)
        {
            GameObject effect = Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(effect, 0.1f);
        }
        Destroy(gameObject);
    }
}
