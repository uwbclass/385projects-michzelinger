using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsControl : MonoBehaviour
{
   [SerializeField] GameObject keysText;
   [SerializeField] GameObject missileKeysText;
  // [SerializeField] GameObject heroCanvas;
   [SerializeField] GameObject optionsMenu;
   [SerializeField] GameObject spriteMask;
   private static bool OptionsEnabled = false;
   // Start is called before the first frame update
   void Start()
   {
      optionsMenu.SetActive(false);
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
      spriteMask.SetActive(true);
      //heroCanvas.SetActive(false);
      keysText.transform.localScale = new Vector3(0f,0f,0f);
      missileKeysText.transform.localScale = new Vector3(0f,0f,0f);
      optionsMenu.transform.localScale = new Vector3(0.8327084f, 0.8327084f, 1f);
      optionsMenu.SetActive(true);
      Time.timeScale = 0f;
   }

   public void HideOptions()
   {
      //Debug.Log("Option hidden");
      //optionsMenu.transform.localScale = new Vector3(0f, 0f, 0f);
     // heroCanvas.SetActive(true);
      keysText.transform.localScale = new Vector3(1f,1f,1f);
      missileKeysText.transform.localScale = new Vector3(1f,1f,1f);
      spriteMask.SetActive(false);
      optionsMenu.SetActive(false);
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
