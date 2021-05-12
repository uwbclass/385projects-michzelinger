using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy_Prototype
{
    protected const float idleTime = 2f;
    protected const float aimTime = 3f;
    protected const float escapeTime = 5f;
    
    private float timer = 0;
    protected float escapeDistance = 2f;
    
    // private float bulletTimeStamp;
    // public float bulletRate = 3.0f;
    public LineRenderer lineRenderer;
    LayerMask layerMask;

    protected override void Start()
    {
        base.Start();
        layerMask = LayerMask.GetMask("Player");
    }

    protected override void ServicePatrolState()
    {
        if(proximity(aggroDistance))
        {
            state = EnemyState.aimingState;
        }
        base.ServicePatrolState();
    }

    protected override void ServiceEscapeState()
    {
        if(proximity(aggroDistance * 1.5f))
        {
            rb2d.velocity = (currPos - playerPos).normalized * 5.0f;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            state = EnemyState.aimingState;
        }
    }

    protected override void ServiceAimingState()
    {
        if(proximity(escapeDistance))
        {
            timer = 0;
            state = EnemyState.escapeState;
            return;   
        }

        if(timer < aimTime)
        {
            proximity(aggroDistance);
            transform.up = (playerPos - currPos).normalized;
            timer += Time.deltaTime;
        }
        else
        {
            StartCoroutine(FireRay());
            timer = 0;
            state = EnemyState.aimingState;
        }
    }

    IEnumerator FireRay()
    {
    
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, player.transform.position);


        lineRenderer.enabled = true;

        yield return new WaitForSeconds(0.05f);

        lineRenderer.enabled = false;
    }
}

