using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidBoss_Enemy : Enemy_Prototype
{
    private float cooldown1;
    private float cooldown2;
    private float cooldownMiddle;
    public float attack1 = 4.0f;
    public float attack2 = 6.0f;
    public float middle = 10.0f;
    public GameObject middleLaser;
    public GameObject Laser1;
    public GameObject Laser2;
    [SerializeField] Transform laser1top;
    [SerializeField] Transform laser1bot;
    [SerializeField] Transform laser2top;
    [SerializeField] Transform laser2bot;
    
    protected override void Start()
    {
        base.Start();
        cooldown1 = attack1;
        cooldown2 = attack2;
        cooldownMiddle = middle;
        isSpawn = false;
    }
    protected override void ServiceAttackState()
    {

        verticalMovement();
        if(Time.time >= cooldown1)
        {
            //Debug.Log("Bullet Time" + bulletTimeStamp);

            Instantiate(Laser1, laser1top.transform.position, transform.rotation);
            Instantiate(Laser1, laser1bot.transform.position, transform.rotation);

            cooldown1 = Time.time + attack1;
        }
        if(Time.time >= cooldown2)
        {
            //Debug.Log("Bullet Time" + bulletTimeStamp);
        //    Instantiate(Laser2, laser2top.transform.position, transform.rotation);
            Instantiate(Laser2, laser2top.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 30f));
            Instantiate(Laser2, laser2top.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -30f));
            Instantiate(Laser2, laser2top.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -10f));
            Instantiate(Laser2, laser2top.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 10f));
            // Instantiate(Laser2, laser2bot.transform.position, transform.rotation);
            Instantiate(Laser2, laser2bot.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 30f));
            Instantiate(Laser2, laser2bot.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -30f));
            Instantiate(Laser2, laser2bot.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, -10f));
            Instantiate(Laser2, laser2bot.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 10f));

            cooldown2 = Time.time + attack2;
        }
        if(Time.time >= cooldownMiddle)
        {
            StartCoroutine(MainLaser());
            cooldownMiddle = Time.time + middle;
        }
    }

    IEnumerator MainLaser()
    {
        middleLaser.SetActive(true);
        yield return new WaitForSeconds(2f);
        middleLaser.SetActive(false);
    }

    protected override void ServicePatrolState()
    {
        if(proximity(aggroDistance * 1.5f))
        {
            state = EnemyState.attackState;
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

