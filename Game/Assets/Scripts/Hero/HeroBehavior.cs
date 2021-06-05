using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroBehavior : Health
{
   public static HeroBehavior instance;
   public static void Respawn()
   {
      Destroy(instance.gameObject);
   }

   [Header("Player")]
   public float moveSpeed = 5f;
   public HealthBar healthBar;
   public float invulnerableTime = 1f;

   SpriteRenderer spriteRenderer;
   public GameObject trailEffect;
   public GameObject smokeEffect;
   public GameObject deathEffect;
   public Collider2D collider2d;
   public Rigidbody2D rb2d;
   public bool canMove = true;

   public const float MaxEnergy = 100f;
   public float energy = MaxEnergy;


   [Header("Projectile")]
   public GameObject laserPrefab;
   private float projectileFiringPeriod = 0.2f;
   public Transform firePoint;
   float cooldown = 0;
   public int missileAmmo;
   Coroutine firingCoroutine;

   public GameObject missilePrefab;

   [Header("UI")]
   Vector3 mouse;
   public GameObject shield;
   public PowerupDisplay iconsDisplayer;
   public bool isSpeed;
   public float speedBoostDuration = 3f;
   public float speedBoostStopTime;

   void Awake()
   {
      if(instance == null)
      {
         instance = this;
         DontDestroyOnLoad(gameObject);
      }
      else
      {
         Destroy(gameObject);
      }
   }

   // Start is called before the first frame update
   protected override void Start()
   {
      base.Start();

      //healthBar.SetHealth(.health, .MaxHealth);
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
         projectileFiringPeriod = 0.2f;
         laserPrefab.GetComponent<LaserBehavior>().laserSpeed = 4f;
         iconsDisplayer.SpeedDisplay(false);
         isSpeed = false;
         trailEffect.SetActive(false);
      }
      Fire();
      energy += (isSpeed ? 50 : 15) * Time.deltaTime;
      if(energy > MaxEnergy) { energy = MaxEnergy; }
   }
   public void EnableSpeedBoost()
   {
      speedBoostStopTime = Time.time + speedBoostDuration;
      moveSpeed++;
      projectileFiringPeriod = 0.1f;
      laserPrefab.GetComponent<LaserBehavior>().laserSpeed = 6f;
      trailEffect.SetActive(true);
      iconsDisplayer.SpeedDisplay(true);
      isSpeed = true;
   }

   void Fire()
   {
      if (Time.timeScale != 0 && cooldown == 0 && energy >= 5f && Input.GetKey(KeyCode.Mouse0))
      {
         GameObject laser = Instantiate(laserPrefab, firePoint.position, transform.rotation);
         cooldown = projectileFiringPeriod;
         energy -= 5f;
      }
      else
      {
         cooldown = cooldown <= 0 ? 0 : cooldown - Time.deltaTime;
      }

      if (Input.GetKeyDown(KeyCode.Mouse1) && missileAmmo > 0 && energy >= 30f)
      {
         Instantiate(missilePrefab, firePoint.position, transform.rotation);
         --missileAmmo;
         if(missileAmmo != 0)
         {
            iconsDisplayer.UpdateBombNumber(missileAmmo);
         }
         else
         {
            iconsDisplayer.BombHide();
         }
         energy -= 30f;
      }
   }

   void FixedUpdate()
   {
      if(canMove)
      {
         Move();
         Rotate();
      }
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
      if (collider.gameObject.layer == 9) // Enemy Laser
      {
         if(shield.activeInHierarchy == true)
         {
            shield.SetActive(false);
            iconsDisplayer.ShieldDisplay(false);
         }
         else
         {
            LaserBehavior laser;
            if(collider.gameObject.TryGetComponent<LaserBehavior>(out laser))
            {
               loseHealth(laser.damageMultiplier);
            }
            else
            {
               loseHealth(3);
            }
         }
      }
      else if (collider.gameObject.layer == 10) // Wormhole
      {
         canMove = false;
         collider2d.enabled = false;
         rb2d.angularVelocity = 0f;
         rb2d.AddTorque(20f);
         GetComponent<Animator>().SetTrigger("shrink");
         StartCoroutine(FallIntoWormhole(collider.transform.position));
      }
   }

   void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.layer == 8 || collision.gameObject.layer == 11 || collision.gameObject.layer == 15) // Enemy, Asteroids, Mine
      {
         if (shield.activeInHierarchy == true)
         {
            shield.SetActive(false);
            iconsDisplayer.ShieldDisplay(false);
            if(collision.gameObject.tag == "Mine") collision.gameObject.GetComponent<Mine>().HeroCollision();
         }
         else
         {
            StartCoroutine(Invulnerable());
            switch (collision.gameObject.tag)
            {
               case "RegularEnemy":
                  loseHealth(2); break;
               case "SuicideBomber":
                  loseHealth(3); break;
               case "Sniper":
                  loseHealth(2); break;
               case "MidBoss":
                  loseHealth(4); break;
               case "Mine":
                  collision.gameObject.GetComponent<Mine>().HeroCollision(); loseHealth(5); break;
               default:    // Handles asteroid
                  loseHealth(1); break;
            }
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

   IEnumerator FallIntoWormhole(Vector2 target)
   {
      float timeElapsed = 0f;
      float endTime = 1.5f;
      Vector2 originalPos = rb2d.position;
      while(timeElapsed < endTime)
      {
         rb2d.MovePosition(Vector2.Lerp(originalPos, target, timeElapsed / endTime));
         timeElapsed += Time.deltaTime;
         yield return null;
      }

      // Reset at the beginning of the level
      transform.position = new Vector3(-6.87f, 0f, 0f);
      collider2d.enabled = true;
      rb2d.angularVelocity = 0;
      canMove = true;

      StartCoroutine(Invulnerable());
   }

   private void Die()
   {
      //collider2d.enabled = false;
      GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
      Destroy(effect, 0.4f);

      FindObjectOfType<WormholeController>().GameOver();
      gameObject.SetActive(false);
   }

   public void gainHealth(int multiplier)
   {
      increaseHealth(multiplier);
      healthBar.SetHealth(health, MaxHealth);
      if ((float) health / MaxHealth > 0.3f)
      {
         smokeEffect.SetActive(false);
      }
   }

   public void loseHealth(int multiplier)
   {
      Camera.main.gameObject.GetComponentInParent<CameraController>().CallShake();
      if ((float) health / MaxHealth <= 0.3f)
      {
         smokeEffect.SetActive(true);
      }
      decreaseHealth(multiplier);
      healthBar.SetHealth(health, MaxHealth);
      
      if (isDead())
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
