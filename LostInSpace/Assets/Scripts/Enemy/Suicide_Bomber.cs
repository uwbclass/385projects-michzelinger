using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide_Bomber : Enemy_Prototype
{
    protected override void ServiceAttackState()
    {
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        currPos = Vector2.MoveTowards(currPos, playerPos, moveSpeed * Time.deltaTime);
        transform.up = Vector3.Normalize(playerPos - currPos);

        transform.position = currPos;
    }

    protected override void ServicePatrolState()
    {
        if(proximity(aggroDistance))
        {
            state = EnemyState.attackState;
            return;
        }
        base.ServicePatrolState();
    }
}
