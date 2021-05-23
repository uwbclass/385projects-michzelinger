using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBoss_Enemy : Enemy_Prototype
{
    private float bulletTimeStamp;
    public float attack1 = 4.0f;
    public float attack2 = 6.0f;
    public GameObject Laser1;
    public GameObject Laser2;
    [SerializeField] Transform laser1top;
    [SerializeField] Transform laser1bot;
    [SerializeField] Transform laser2top;
    [SerializeField] Transform laser2bot;

    protected override void ServiceAttackState()
    {
        verticalMovement();
        if(Time.time >= attack1)
        {
            //Debug.Log("Bullet Time" + bulletTimeStamp);

            Instantiate(Laser1, laser1top.transform.position, transform.rotation);
            Instantiate(Laser1, laser1bot.transform.position, transform.rotation);

            attack1 = Time.time + attack1;
        }
        if(Time.time >= attack2)
        {
            //Debug.Log("Bullet Time" + bulletTimeStamp);
            //c
            Instantiate(Laser2, laser2top.transform.position, transform.rotation);
            Instantiate(Laser2, laser2bot.transform.position, transform.rotation);

            attack2 = Time.time + attack2;
        }
    }

    protected override void ServicePatrolState()
    {
        if(proximity(aggroDistance * 3))
        {
            state = EnemyState.attackState;
        }
    }
    
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void verticalMovement()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].position;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }
}

