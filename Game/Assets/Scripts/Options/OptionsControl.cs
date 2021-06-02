using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
         //Debug.Log("esc pressed");
      }
   }
   public void ShowOptions()
   {
      //Debug.Log("Option showed");
      optionsMenu.transform.localScale = new Vector3(0.8327084f, 0.8327084f, 1f);

      Time.timeScale = 0f;
   }

   public void HideOptions()
   {
      //Debug.Log("Option hidden");
      optionsMenu.transform.localScale = new Vector3(0f, 0f, 0f);

      Time.timeScale = 1f;
   }

   public void ReturnToMenu()
   {
      SceneManager.LoadScene("MainMenu");
      Destroy(HeroBehavior.instance.gameObject);
      Destroy(AudioPlayer.instance.gameObject);
      Time.timeScale = 1f;
   }
}
