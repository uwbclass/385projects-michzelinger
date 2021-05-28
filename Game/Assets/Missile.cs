using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject circle;
    private Enemy_Prototype[] enemies;
    private Enemy_Prototype target;

    private float moveSpeed = 5f;
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
            transform.up = (target.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            moveSpeed += 2 * Time.deltaTime;
        }
        else
        {
            transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnTriggerEnter2D()
    {
        GameObject c = Instantiate(circle, transform.position, Quaternion.identity);
        c.SetActive(true);
        Destroy(c, 0.1f);
        Collider2D[] affected = Physics2D.OverlapCircleAll(transform.position, explosionRange, layerMask.value);
        foreach(var a in affected)
        {
            Debug.Log(a.gameObject.transform.position);
            a.gameObject.GetComponent<Enemy_Prototype>().loseHealth(5);
        }
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
