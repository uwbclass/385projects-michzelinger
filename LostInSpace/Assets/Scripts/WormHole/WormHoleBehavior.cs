using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WormHoleBehavior : MonoBehaviour
{
   public GameObject buttonCanvas;
   private float wormHoleCd = 2f;
   private float lastTriggered = -2f;

   void OnTriggerEnter2D()
   {
      if (Time.time > lastTriggered + wormHoleCd)
      {
         lastTriggered = Time.time;
         Debug.Log("active?");
         buttonCanvas.SetActive(true);
         Time.timeScale = 0;
      }
   }
}
