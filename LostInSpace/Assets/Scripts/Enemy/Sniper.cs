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
    public LineRenderer lineRenderer;
    bool started = false;

    protected override void ServicePatrolState()
    {
        if(proximity(aggroDistance))
        {
            state = EnemyState.attackState;
        }
        base.ServicePatrolState();
    }

    protected override void ServiceEscapeState()
    {
        if(proximity(aggroDistance * 1.5f))
        {
            rb2d.velocity = (currPos - playerPos).normalized * 5.0f;
            transform.up = rb2d.velocity.normalized;
        }
        else
        {
            rb2d.velocity = Vector2.zero;
            state = EnemyState.attackState;
        }
    }

    protected override void ServiceAttackState()
    {
        if(proximity(escapeDistance))
        {
            lineRenderer.enabled = false;
            timer = 0;
            state = EnemyState.escapeState;
            return;   
        }

        if(timer < aimTime)
        {
            lineRenderer.enabled = true;
            lineRenderer.endColor = new Color(1, 1 - timer / aimTime, 1 - timer / aimTime, 1);
            lineRenderer.startColor = new Color(1, 1 - timer / aimTime, 1 - timer / aimTime, 1);
            //Debug.Log(lineRenderer.endColor);
            lineRenderer.SetPosition(0, currPos);
            lineRenderer.SetPosition(1, playerPos);
            transform.up = (playerPos - currPos).normalized;
            timer += Time.deltaTime;
        }
        else
        {
            if(!started)
                StartCoroutine(FireRay());
        }
    }

    IEnumerator FireRay()
    {
        started = true;
        // lineRenderer.endColor = Color.white;
        lineRenderer.enabled = false;

        yield return new WaitForSeconds(0.5f);
        lineRenderer.SetPosition(1, transform.up * 1000);
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = false;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startWidth = 0.05f;
        yield return new WaitForSeconds(0.5f);
        timer = 0;
        started = false;
    }
}

