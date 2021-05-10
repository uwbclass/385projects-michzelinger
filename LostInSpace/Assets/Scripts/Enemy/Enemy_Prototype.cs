using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Prototype : MonoBehaviour
{
    //FSM commands
    protected enum EnemyState
    {
        patrolState,
        attackState
    };

    // The parent that contains all the waypoints
    public GameObject pathPrefab;
    public float moveSpeed = 30f;
    public float aggroDistance = 40f;
    
    protected List<Transform> waypoints;
    protected int waypointIndex = 0;
    protected EnemyState state = EnemyState.patrolState;
    protected HeroBehavior player;
    protected Vector2 currPos;
    protected Vector2 playerPos;


    protected virtual void UpdateFSM()
    {
        switch (state)
        {
            case EnemyState.attackState:
                ServiceAttackState(); 
                break;
            case EnemyState.patrolState:
                ServicePatrolState();
                break;
        }
    }

    protected virtual void ServicePatrolState()
    {
        proximity();

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
        proximity();   
        Debug.Log("Attack state not implemented");
    }

    protected virtual void proximity()
    {
        currPos = new Vector2(transform.position.x, transform.position.y);
        playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        if(Vector2.Distance(playerPos, currPos) <= aggroDistance)
        {
            state = EnemyState.attackState;
        }
        else
        {
            state = EnemyState.patrolState;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<HeroBehavior>();
        waypoints = new List<Transform>();
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
