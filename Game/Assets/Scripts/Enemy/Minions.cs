using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : Enemy_Prototype
{
    private float bulletTimeStamp;
    public float bulletRate = 0.5f;
    public GameObject Laser;

    protected override void ServiceAttackState()
    {   
        if(!proximity(aggroDistance))
        {
            timeAggroed = false;
            state = EnemyState.patrolState;
            return;
        }
        
        transform.up = Vector3.Normalize(playerPos - currPos);

        if(Time.time >= bulletTimeStamp && state == EnemyState.attackState)
        {
            //Debug.Log("Bullet Time" + bulletTimeStamp);

            GameObject b = Instantiate(Laser, transform.position, transform.rotation);

            bulletTimeStamp = Time.time + bulletRate;
        }
    }

    protected override void ServicePatrolState()
    {
        if(proximity(aggroDistance))
        {
            state = EnemyState.attackState;
            return;
        }
        
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        currPos = Vector2.MoveTowards(currPos, playerPos, moveSpeed * Time.deltaTime);
        transform.up = Vector3.Normalize(playerPos - currPos);

        transform.position = currPos;
    }
}
