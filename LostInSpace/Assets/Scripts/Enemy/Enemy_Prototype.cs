using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Prototype : MonoBehaviour
{
    //FSM commands
    protected enum EnemyState
    {
        attackState,
        escapeState,      
        patrolState
    };

    //----------------movement----------------
    public GameObject pathPrefab; // The parent that contains all the waypoints
    protected List<Transform> waypoints;
    protected int waypointIndex = 0;    
    protected float moveSpeed = 3.0f;
    protected Rigidbody2D rb2d;    

    //----------------player tracking----------------
    protected HeroBehavior player;
    protected Vector2 currPos;
    protected Vector2 playerPos;

    //----------------state variables----------------
    protected EnemyState state = EnemyState.patrolState;
    protected float aggroDistance = 4.0f;


    // FSM Core
    protected virtual void UpdateFSM()
    {
        switch (state)
        {
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

    protected virtual void ServicePatrolState()
    {
        // Incomplete, need to implement state change logic in child class
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

    // Sniper specific state function
    protected virtual void ServiceEscapeState() {}

    //If player is within "distance" from enemy
    protected virtual bool proximity(float distance)
    {
        //Debug.Log(Vector2.Distance(playerPos, currPos));
        return (Vector2.Distance(playerPos, currPos) <= distance);
    }

    // Updates the currPos and playerPos for easy tracking
    protected virtual void UpdatePositions()
    {
        currPos.Set(transform.position.x, transform.position.y);
        playerPos.Set(player.transform.position.x, player.transform.position.y);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        // Setting up state variables
        player = FindObjectOfType<HeroBehavior>();
        waypoints = new List<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        currPos = new Vector2();
        playerPos = new Vector2();
        foreach(Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        // Start at initial waypoint
        transform.position = waypoints[0].position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdatePositions();
        UpdateFSM();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
