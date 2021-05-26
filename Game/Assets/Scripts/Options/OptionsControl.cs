using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsControl : MonoBehaviour
{
   [SerializeField] GameObject optionsMenu;
   private static bool OptionsEnabled = false;
   // Start is called before the first frame update
   void Start()
   {

   }

   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (OptionsEnabled)
         {
            
            HideOptions();
            OptionsEnabled = false;
         }
         else
         {
            ShowOptions();
            OptionsEnabled = true;
         }
         Debug.Log("esc pressed");
      }
   }
   public void ShowOptions()
   {
      Debug.Log("Option showed");
      optionsMenu.SetActive(true);
      Time.timeScale = 0f;
   }

   public void HideOptions()
   {
      Debug.Log("Option hidden");
      optionsMenu.SetActive(false);
      Time.timeScale = 1f;
   }
}
