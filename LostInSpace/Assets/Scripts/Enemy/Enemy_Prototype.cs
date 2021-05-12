using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Prototype : MonoBehaviour
{
    //FSM commands
    protected enum EnemyState
    {
        aimingState,
        attackState,
        escapeState,      
        patrolState
    };

    // The parent that contains all the waypoints
    public GameObject pathPrefab;
    protected float moveSpeed = 3.0f;
    protected float aggroDistance = 4.0f;
    // 
    protected List<Transform> waypoints;
    protected int waypointIndex = 0;
    protected EnemyState state = EnemyState.patrolState;
    protected HeroBehavior player;
    protected Vector2 currPos;
    protected Vector2 playerPos;
    protected Rigidbody2D rb2d;


    protected virtual void UpdateFSM()
    {
        switch (state)
        {
            case EnemyState.aimingState:
                ServiceAimingState();
                break;
            case EnemyState.attackState:
                ServiceAttackState();
                break;
            case EnemyState.escapeState:
                ServiceEscapeState();
                break;      
            case EnemyState.patrolState:
                ServicePatrolState();
                break;
        }
    }

    // General functions
    protected virtual void ServicePatrolState()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].position;

            transform.up = (targetPosition - transform.position).normalized;

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

    protected virtual void ServiceAttackState()
    {
        Debug.Log("Attack state not implemented");
    }

    // Sniper specific functions
    protected virtual void ServiceEscapeState() {}

    protected virtual void ServiceAimingState() {}

    //If player is within "distance" from enemy
    protected virtual bool proximity(float distance)
    {
        currPos = new Vector2(transform.position.x, transform.position.y);
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        //Debug.Log(Vector2.Distance(playerPos, currPos));
        return (Vector2.Distance(playerPos, currPos) <= distance);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<HeroBehavior>();
        waypoints = new List<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateFSM();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
