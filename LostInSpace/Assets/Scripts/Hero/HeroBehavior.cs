using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehavior : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb2d;
    public Health myHealth;
    public HealthBar healthBar;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    [SerializeField] Transform firePoint;
    float cooldown = 0;
    Coroutine firingCoroutine;
    
    [Header("UI")]
    Vector3 mouse;
    public GameObject shield;
    public bool isSpeed;
    public float speedBoostDuration = 3f;
    public float speedBoostStopTime;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        healthBar.SetHealth(myHealth.health, myHealth.MaxHealth);
        isSpeed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpeed && Time.time >= speedBoostStopTime)
        {  
            moveSpeed = 3f;
            isSpeed = false;
        }
        Fire();
    }

    public void EnableSpeedBoost()
    {
        speedBoostStopTime = Time.time + speedBoostDuration;
        moveSpeed++;
        isSpeed = true;
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
            healthBar.SetHealth(myHealth.health, myHealth.MaxHealth);
        }
    }
}
