using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Enemy : Enemy_Prototype
{
    private float bulletTimeStamp;
    public float bulletRate = 2.0f;
    public GameObject Laser;

    protected override void ServiceAttackState()
    {   
        proximity();
        transform.up = Vector3.Normalize(playerPos - currPos);

        if(Time.time >= bulletTimeStamp && state == EnemyState.attackState)
        {
            //Debug.Log("Bullet Time" + bulletTimeStamp);

            GameObject b = Instantiate(Laser, transform.position, transform.rotation);

            bulletTimeStamp = Time.time + bulletRate;
        }
    }
}
