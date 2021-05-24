using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroBehavior : MonoBehaviour
{
   [Header("Player")]
   [SerializeField] float moveSpeed = 5f;
   public Health myHealth;
   public HealthBar healthBar;
   public float invulnerableTime = 1f;
   SpriteRenderer spriteRenderer;
   public GameObject trailEffect;
   public GameObject smokeEffect;
   [SerializeField] GameObject deathEffect;
   public Collider2D collider2d;
   public Rigidbody2D rb2d;


   [Header("Projectile")]
   [SerializeField] GameObject laserPrefab;
   [SerializeField] float projectileFiringPeriod = 0.5f;
   [SerializeField] Transform firePoint;
   float cooldown = 0;
   Coroutine firingCoroutine;

   [Header("UI")]
   Vector3 mouse;
   public GameObject shield;
   public PowerupDisplay speedIcon;
   public bool isSpeed;
   public float speedBoostDuration = 3f;
   public float speedBoostStopTime;

   // Start is called before the first frame update
   void Start()
   {
      myHealth = GetComponent<Health>();
      //healthBar.SetHealth(myHealth.health, myHealth.MaxHealth);
      isSpeed = false;
      spriteRenderer = GetComponent<SpriteRenderer>();

      StartCoroutine(Invulnerable());
   }

   // Update is called once per frame
   void Update()
   {
      if (isSpeed && Time.time >= speedBoostStopTime)
      {
         moveSpeed = 3f;
         speedIcon.SpeedDisplay(false);
         isSpeed = false;
         trailEffect.SetActive(false);
      }
      Fire();
   }

   public void EnableSpeedBoost()
   {
      speedBoostStopTime = Time.time + speedBoostDuration;
      moveSpeed++;
      trailEffect.SetActive(true);
      speedIcon.SpeedDisplay(true);
      isSpeed = true;
   }

   void Fire()
   {
      if (Time.timeScale != 0 && cooldown == 0 && Input.GetKey(KeyCode.Mouse0))
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
      if (shield.activeInHierarchy == true && collider.gameObject.layer == 9)
      {
         shield.SetActive(false);
      }
      else if (collider.gameObject.layer == 9)
      {
         loseHealth(collider.gameObject.GetComponent<LaserBehavior>().damageMultiplier);
      }
   }

   void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11)
      {
         if (shield.activeInHierarchy == true)
         {
            shield.SetActive(false);
         }
         else
         {
            switch (collision.gameObject.tag)
            {
               case "RegularEnemy":
                  loseHealth(2); break;
               case "SuicideBomber":
                  loseHealth(4); break;
               case "Sniper":
                  loseHealth(2); break;
               case "MidBoss":
                  loseHealth(4); break;
               default:    // Handles asteroid
                  loseHealth(1); break;
            }
            StartCoroutine(Invulnerable());
         }
      }
      // else if(collision.gameObject.layer == 11)
      // {
      //     if(shield.activeInHierarchy == true)
      //     {
      //         shield.SetActive(false);
      //     }
      //     else
      //     {
      //         loseHealth(1);
      //         StartCoroutine(Invulnerable());
      //     }
      // }
   }

   private void Die()
   {
      collider2d.enabled = false;
      GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
      Destroy(effect, 0.4f);
      FindObjectOfType<WormholeController>().loadGameOver();
      gameObject.SetActive(false);
   }



   public void loseHealth(int multiplier)
   {
      Camera.main.gameObject.GetComponent<CameraShake>().CallShake();
      if ((float)myHealth.health / myHealth.MaxHealth <= 0.3f)
      {
         smokeEffect.SetActive(true);
      }
      myHealth.decreaseHealth(multiplier);
      healthBar.SetHealth(myHealth.health, myHealth.MaxHealth);
      if (myHealth.isDead())
      {
         Die();
      }
   }

   IEnumerator Invulnerable()
   {
      float endTime = Time.time + invulnerableTime;

      collider2d.enabled = false;

      while (Time.time < endTime)
      {
         spriteRenderer.color = new Color(1f, 1f, 1f, 0.2f);
         yield return new WaitForSeconds(0.05f);
         spriteRenderer.color = Color.white;
         yield return new WaitForSeconds(0.05f);
      }

      collider2d.enabled = true;
   }
}
