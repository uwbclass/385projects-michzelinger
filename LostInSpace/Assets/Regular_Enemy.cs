using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regular_Enemy : MonoBehaviour
{
    //FSM commands
    private enum EnemyState
    {
        patrolState,
        attackState
    };

    // The parent that contains all the waypoints
    public GameObject pathPrefab;
    public float moveSpeed = 30f;
    public float aggroDistance = 40f;
    private List<Transform> waypoints;
    private int waypointIndex = 0;
    private EnemyState state = EnemyState.patrolState;
    private HeroBehavior player;
    private float bulletTimeStamp;
    public float bulletRate = 2.0f;
    public GameObject Laser;

    Vector2 currPos;
    Vector2 playerPos;

    

    private void UpdateFSM()
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

    private void ServicePatrolState()
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

    private void ServiceAttackState()
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
    private void proximity()
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
    void Start()
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
    void Update()
    {
        UpdateFSM();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            Destroy(gameObject);
    }
}
