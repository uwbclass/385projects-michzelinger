using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final_Boss : Enemy_Prototype
{
    public Transform firePt1;
    public Transform firePt2;
    private float cooldown1;
    private float cooldown2;
    private float cooldown3;
    public float laserRate = 4.0f;
    // public float middle = 8.0f;
    // public GameObject middleLaser;
    // public GameObject aimer;
    public GameObject Mines;
    public GameObject Laser;
    private List<Transform> laserArray;
    [SerializeField] Transform laserTop1;
    [SerializeField] Transform laserTop2;
    [SerializeField] Transform laserBot1;
    [SerializeField] Transform laserBot2;

    public GameObject enemy;
    
    protected override void Start()
    {
        // Base start
        player = HeroBehavior.instance;
        waypoints = new List<Transform>();
        rb2d = GetComponent<Rigidbody2D>();
        currPos = new Vector2();
        playerPos = new Vector2();
        cooldown1 = 0;
        
        foreach (Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        laserArray = new List<Transform>();
        laserArray.Add(laserTop1);
        laserArray.Add(laserTop2);
        laserArray.Add(laserBot1);
        laserArray.Add(laserBot2);

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
            GameObject m = Instantiate(Mines, new Vector3(firePt1.position.x, Random.Range(firePt2.position.y, firePt1.position.y), 1f), transform.rotation);
            m.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 0f);
            float attack1 = Random.Range(0.5f, 5f);
            cooldown1 = Time.time + attack1;
        }
        if(Time.time >= cooldown2)
        {
            Summon();

            cooldown2 = Time.time + Random.Range(8f, 10f);
        }
        if(Time.time >= cooldown3)
        {
            Transform temp = laserArray[Random.Range(0, laserArray.Count)];
            Instantiate(Laser, temp.position, transform.rotation);
            // Instantiate(Laser, laserTop1.transform.position, transform.rotation);
            // Instantiate(Laser, laserTop2.transform.position, transform.rotation);
            // Instantiate(Laser, laserBot1.transform.position, transform.rotation);
            // Instantiate(Laser, laserBot2.transform.position, transform.rotation);
            laserRate = Random.Range(0.5f,2f);
            cooldown3 = Time.time + laserRate;
        }
        // if(Time.time >= cooldownMiddle)
        // {
        //     StartCoroutine(MainLaser());
        //     cooldownMiddle = Time.time + middle;
        // }
    }

    private void Summon()
    {
        Instantiate(enemy, new Vector3(firePt1.position.x, Random.Range(firePt2.position.y, firePt1.position.y), 1f), transform.rotation);
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

