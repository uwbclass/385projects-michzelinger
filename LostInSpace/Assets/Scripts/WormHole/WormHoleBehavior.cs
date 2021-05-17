using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WormHoleBehavior : MonoBehaviour
{
   public Animator transitionAnim;

   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.name == "Hero")
      {
         StartCoroutine(LoadScene());
      }
   }
   // void Update()
   // {
   //    transform.Rotate(0,0,50*Time.deltaTime);
   // }

   IEnumerator LoadScene()
   {
      transitionAnim.SetTrigger("end");
      yield return new WaitForSeconds(0.75f);
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
}
