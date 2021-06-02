using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{   
    public GameObject circle;
    private float explosionRange = 1f;
    private LayerMask layerMask;

    private SpriteRenderer sr;
    private const float flashTime = 1f;
    private float nextFlash;
    // Start is called before the first frame update
    void Start()
    {
        circle.transform.localScale = new Vector3(explosionRange * 2, explosionRange * 2, 1);
        layerMask = LayerMask.GetMask("Player");
        sr = GetComponent<SpriteRenderer>();
        nextFlash = Time.time + flashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextFlash)
        {
            nextFlash = Time.time + flashTime;
            Flash();
        }
    }
    void Flash()
    {
        if(sr.color == Color.white)
        {
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    void OnTriggerEnter2D()
    {
        GameObject c = Instantiate(circle, transform.position, Quaternion.identity);
        c.SetActive(true);
        Destroy(c, 0.5f);
        Collider2D affected = Physics2D.OverlapCircle(transform.position, explosionRange, layerMask.value);
        if(affected != null) affected.GetComponent<HeroBehavior>().loseHealth(5);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
