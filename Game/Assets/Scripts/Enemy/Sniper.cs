using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Enemy_Prototype
{
    protected const float idleTime = 2f;
    protected const float aimTime = 1f;
    protected const float escapeTime = 5f;
    
    private float timer = 0;
    protected float escapeDistance = 2f;
    public LineRenderer lineRenderer;
    LayerMask layerMask;
    bool started = false;

    protected override void Update()
    {
        base.Update();
        rb2d.angularVelocity = 0f;
    }
    
    protected override void Start()
    {
        base.Start();
        layerMask = LayerMask.GetMask("Player");
    }
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
        if(timeAggroed && Time.time > deAggroTime)
        {
            timeAggroed = false;
            lineRenderer.enabled = false;
            timer = 0;
            state = EnemyState.patrolState;
            return;
        }

        if(!timeAggroed && !proximity(aggroDistance * 3f))
        {
            lineRenderer.enabled = false;
            timer = 0;
            state = EnemyState.patrolState;
            return;
        }

        if(proximity(escapeDistance))
        {
            StopAllCoroutines();
            lineRenderer.enabled = false;
            started = false;
            lineRenderer.endWidth = 0.02f;
            lineRenderer.startWidth = 0.02f;
            timer = 0;
            state = EnemyState.escapeState;
            return;   
        }

        if(timer < aimTime)
        {
            lineRenderer.enabled = true;
            lineRenderer.endColor = new Color(1, 1 - timer / aimTime, 1 - timer / aimTime, 1);
            lineRenderer.startColor = new Color(1, 1 - timer / aimTime, 1 - timer / aimTime, 1);
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

        // Delay from aim to shoot
        yield return new WaitForSeconds(0.5f);

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, 1000f, layerMask.value);
        if(hitInfo)
        {
            if(hitInfo.transform.gameObject.layer == 6)
            {
                HeroBehavior.instance.loseHealth(2);
            }
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(1, transform.up * 1000);
        }
        
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f); // Length of the actual shooting
        lineRenderer.enabled = false;
        lineRenderer.endWidth = 0.02f;
        lineRenderer.startWidth = 0.02f;
        yield return new WaitForSeconds(1f); // Cooldown between shooting and aiming again
        timer = 0;
        started = false;
    }
}

