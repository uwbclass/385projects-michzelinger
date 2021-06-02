using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Boss : Enemy_Prototype
{
    public Transform firePt1;
    public Transform firePt2;
    private float cooldown1;
    private float cooldown2;
    // private float cooldownMiddle;
    // public float attack2 = 6.0f;
    // public float middle = 8.0f;
    // public GameObject middleLaser;
    // public GameObject aimer;
    public GameObject Laser1;
    // public GameObject Laser2;
    // [SerializeField] Transform laser1top;
    // [SerializeField] Transform laser1bot;
    // [SerializeField] Transform laser2top;
    // [SerializeField] Transform laser2bot;

    public GameObject enemy;

    private List<GameObject> objects;
    
    private void FixedUpdate()
    {
        foreach(var obj in objects)
        {
            
        }
    }
    protected override void Start()
    {
        base.Start();
        objects = new List<GameObject>();
        cooldown1 = 0;
        // cooldown2 = 0;
        // cooldownMiddle = 0;
        isSpawn = false;
    }
    protected override void ServiceAttackState()
    {

        verticalMovement();
        if(Time.time >= cooldown1)
        {
            //Debug.Log("Bullet Time" + bulletTimeStamp);
            // Debug.Log(firePt1.position + " " + firePt2.position);
            GameObject mine = Instantiate(Laser1, new Vector3(firePt1.position.x, Random.Range(firePt2.position.y, firePt1.position.y), 1f), transform.rotation);
            objects.Add(mine);

            float attack1 = Random.Range(0.5f, 5f);
            cooldown1 = Time.time + attack1;
        }
        if(Time.time >= cooldown2)
        {
            Summon();

            cooldown2 = Time.time + Random.Range(1f, 10f);
        }
        // if(Time.time >= cooldownMiddle)
        // {
        //     StartCoroutine(MainLaser());
        //     cooldownMiddle = Time.time + middle;
        // }
    }

    private void Summon()
    {
        GameObject obj = Instantiate(enemy, new Vector3(firePt1.position.x, Random.Range(firePt2.position.y, firePt1.position.y), 1f), transform.rotation);
        objects.Add(obj);
    }

    // IEnumerator MainLaser()
    // {
    //     aimer.SetActive(true);
    //     yield return new WaitForSeconds(2f);
    //     middleLaser.SetActive(true);
    //     aimer.SetActive(false);
    //     yield return new WaitForSeconds(2f);
    //     middleLaser.SetActive(false);    
    // }

    protected override void ServicePatrolState()
    {
        if(proximity(aggroDistance * 1.5f))
        {
            state = EnemyState.attackState;
            // cooldown1 = Time.time + attack1;
            // cooldown2 = Time.time + attack2;
            // cooldownMiddle = Time.time + middle;
            GetComponent<Collider2D>().enabled = true;
        }
    }
    
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void verticalMovement()
    {
        if (waypointIndex < waypoints.Count)
        {
            var targetPosition = waypoints[waypointIndex].position;

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
}

