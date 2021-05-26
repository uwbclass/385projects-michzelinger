using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WormHoleBehavior : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.name == "Hero")
      {
         collision.gameObject.GetComponent<Animator>().SetTrigger("shrink");
         GetComponentInParent<WormholeController>().NextScene();
      }
   }
}
