using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb2d;
    Health myHealth;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    [SerializeField] Transform firePoint;
    float cooldown = 0;
    Coroutine firingCoroutine;
    
    [Header("UI")]
    Vector3 mouse;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(Time.timeScale != 0 && cooldown == 0 && Input.GetKey(KeyCode.Mouse0))
        {
            GameObject laser = Instantiate(laserPrefab, firePoint.position, transform.rotation);
            cooldown = projectileFiringPeriod;
        }
        else
        {
            cooldown = cooldown <= 0 ? 0 : cooldown - Time.deltaTime; 
        }
    }

    void FixedUpdate()
    {
        Move();
        Rotate();  
    }

    void Rotate()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb2d.MoveRotation(Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - rb2d.position.y, mouse.x - rb2d.position.x) * Mathf.Rad2Deg - 90f));
    }
    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb2d.MovePosition(rb2d.position + input * Time.fixedDeltaTime * moveSpeed);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(shield.activeInHierarchy == true && (collider.gameObject.layer == 9 || collider.gameObject.layer == 8))
        {
            shield.SetActive(false);
        }
        else if(collider.gameObject.layer == 9)
        {
            Debug.Log("Health reduced");
            myHealth.decreaseHealth();
            // if(myHealth.isDead())
            // {
            //     Destroy(gameObject);
            // }
        }
    }
}
