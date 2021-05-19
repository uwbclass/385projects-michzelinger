using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   public enum powerUpType
   {
      shield,
      speed,
      health
   }

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
         HeroBehavior player = collider.GetComponent<HeroBehavior>();
         switch(type)
         {
            case powerUpType.shield:
               player.shield.SetActive(true);
               break;
            case powerUpType.speed:
               player.EnableSpeedBoost();
               break;
            case powerUpType.health:
               player.myHealth.increaseHealth();
               player.healthBar.SetHealth(player.myHealth.health, player.myHealth.MaxHealth);
               break;
            default:
               break;
         }

         Destroy(gameObject);
      }
   }
}
