using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject circle;
    private Enemy_Prototype[] enemies;
    private Enemy_Prototype target;

    private float moveSpeed = 5f;
    private float rotateSpeed = 60f;
    private float explosionRange = 1f;
    private LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        circle.transform.localScale = new Vector3(explosionRange * 2, explosionRange * 2, 1);

        layerMask = LayerMask.GetMask("Enemy");

        float closest = float.MaxValue;
        enemies = FindObjectsOfType<Enemy_Prototype>();
        foreach(var e in enemies)
        {
            float d = Mathf.Abs(Vector2.Distance(e.transform.position, transform.position));
            if(d < closest)
            {
                closest = d;
                target = e;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            // transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            // moveSpeed += 2 * Time.deltaTime;

            transform.position += transform.up * moveSpeed * Time.deltaTime;
            moveSpeed += 2 * Time.deltaTime;

            // Rotate
            Vector2 lookDirection = (Vector2) (target.transform.position - transform.position);
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion qTo = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, qTo, rotateSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
        }
    }
    
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D()
    {
        GameObject c = Instantiate(circle, transform.position, Quaternion.identity);
        c.SetActive(true);
        Destroy(c, 0.5f);
        Collider2D[] affected = Physics2D.OverlapCircleAll(transform.position, explosionRange, layerMask.value);
        foreach(var a in affected)
        {
            //Debug.Log(a.gameObject.transform.position);
           // if(a.gameObject.tag == "Mine") Destroy (a.gameObject);
            if(a.gameObject.tag != "Mine")
                a.gameObject.GetComponent<Enemy_Prototype>().loseHealth(5);
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
