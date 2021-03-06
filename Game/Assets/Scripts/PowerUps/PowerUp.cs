using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   public AudioClip pickupSound;
   public static bool displayMissileKey = false;
   public static void SpawnRandom(Vector3 spawnPos)
   {
      // int randomOdds;

      // int spawnOdds = 3;
      // randomOdds = Random.Range(1, 4);

      if(Random.Range(1, 4) == 3)
      {
         Object.Instantiate(Resources.Load("Prefabs/Powerups/Pup") as GameObject, spawnPos, Quaternion.identity);
      }
   }

   public enum powerUpType
   {
      shield,
      speed,
      health,
      missile,
      random
   }
   public Sprite[] spriteList;
   public powerUpType type;

   public bool animPos = true;
   public Vector3 posAmplitude = Vector3.one;
   public Vector3 posSpeed = Vector3.one;

   private Vector3 origPos;

   private float startAnimOffset = 0;


   /**
    * Awake
    */
   void Awake()
   {
      if(type == powerUpType.random)
      {
         int whatType = Random.Range(0, 4);
         type = (powerUpType) whatType;
         
      }
      GetComponent<SpriteRenderer>().sprite = spriteList[(int)type];

      
      origPos = transform.position;
      posAmplitude.y = 0.05f;
      posSpeed.y = 2f;
      startAnimOffset = Random.Range(0f, 540f);        // so that the xyz anims are already offset from each other since the start
   }

   /**
    * Update
    */
   void Update()
   {
      /* position */
      if (animPos)
      {
         Vector3 pos;
         pos.x = origPos.x;
         pos.z = origPos.y;
         // pos.x = origPos.x + posAmplitude.x*Mathf.Sin(posSpeed.x*Time.time + startAnimOffset);
         pos.y = origPos.y + posAmplitude.y * Mathf.Sin(posSpeed.y * Time.time + startAnimOffset);
         // pos.z = origPos.z + posAmplitude.z*Mathf.Sin(posSpeed.z*Time.time + startAnimOffset);
         transform.position = pos;
      }
   }

   void OnTriggerEnter2D(Collider2D collider)
   {
      if(collider.gameObject.layer == 6) // player layer
      {
         HeroBehavior player = HeroBehavior.instance;
         AudioPlayer.instance.GetComponent<AudioSource>().PlayOneShot(pickupSound);
         switch(type)
         {
            case powerUpType.shield:
               player.shield.SetActive(true);
               player.iconsDisplayer.ShieldDisplay(true);
               break;
            case powerUpType.speed:
               player.EnableSpeedBoost();
               break;
            case powerUpType.health:
               player.gainHealth(2);
               player.healthBar.SetHealth(player.health, player.MaxHealth);
               break;
            case powerUpType.missile:
               if(displayMissileKey == false)
               {
                  FindObjectOfType<LevelDisplay>().DisplayMissileKey();
                  displayMissileKey = true;
               }
               player.missileAmmo += 3;
               player.iconsDisplayer.BombDisplay(player.missileAmmo);
               break;
            default:
               break;
         }

         Destroy(gameObject);
      }
   }
}
