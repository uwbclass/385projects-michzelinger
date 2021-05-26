using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardWormholeBehavior : MonoBehaviour
{
   public int sceneIndex;

   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.layer == 6)
      {
         GetComponentInParent<WormholeController>().PreviousScene(sceneIndex);
      }
   }
}
