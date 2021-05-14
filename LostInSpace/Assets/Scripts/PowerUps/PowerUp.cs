using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isShield;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D  collider)
    {
        if(collider.tag == "Player")
        {
            if(isShield)
            {
                collider.GetComponent<HeroBehavior>().shield.SetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
