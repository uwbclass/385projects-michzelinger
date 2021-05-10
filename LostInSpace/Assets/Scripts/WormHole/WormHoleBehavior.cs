using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormHoleBehavior : MonoBehaviour
{
   public GameObject buttonCanvas;
   private float wormHoleCd = 2f;
   private float lastTriggered = -2f;

   void OnTriggerEnter2D(Collider2D collision)
   {
      if (Time.time > lastTriggered + wormHoleCd && collision.gameObject.name == "Hero")
      {
         lastTriggered = Time.time;
         Debug.Log("active?");
         buttonCanvas.SetActive(true);
         Time.timeScale = 0;
      }
   }
   void Update()
   {
      transform.Rotate(0,0,50*Time.deltaTime);
   }
}
