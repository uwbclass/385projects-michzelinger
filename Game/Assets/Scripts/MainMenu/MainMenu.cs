using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void ShowOptions()
    {
        optionsMenu.transform.localScale = new Vector3(0.8327084f, 0.8327084f, 1f);
    }

    public void HideOptions()
    {
        optionsMenu.transform.localScale = new Vector3(0f, 0f, 0f);
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
