using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WormHoleBehavior : MonoBehaviour
{
   
   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.name == "Hero")
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
      }
   }
   void Update()
   {
      transform.Rotate(0,0,50*Time.deltaTime);
   }
}
