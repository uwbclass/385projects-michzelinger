using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsControl : MonoBehaviour
{
   // [SerializeField] CanvasGroup TextCanvas;
   // [SerializeField] CanvasGroup HeroCanvas;

  // [SerializeField] GameObject heroCanvas;
   public static CanvasGroup heroCanvas;

   public GameObject textCanvas;
   [SerializeField] GameObject optionsMenu;
   [SerializeField] GameObject spriteMask;
   private static bool OptionsEnabled = false;
   // Start is called before the first frame update
   void Start()
   {
      heroCanvas = GameObject.FindObjectOfType<CanvasGroup>();
      textCanvas = GameObject.Find("Canvas");
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
      spriteMask.SetActive(true);
      textCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
      heroCanvas.blocksRaycasts = false;
      textCanvas.GetComponent<CanvasGroup>().alpha = 0f;
      heroCanvas.alpha = 0f;
      optionsMenu.transform.localScale = new Vector3(0.8327084f, 0.8327084f, 1f);
      optionsMenu.SetActive(true);
      Time.timeScale = 0f;
   }

   public void HideOptions()
   {
      textCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
      heroCanvas.blocksRaycasts = true;
      textCanvas.GetComponent<CanvasGroup>().alpha = 1f;
      heroCanvas.alpha = 1f;
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
