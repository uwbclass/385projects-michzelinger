using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Enemy_Prototype[] enemies;
    private Enemy_Prototype target;

    private float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
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
        Destroy(gameObject);
    }
}
