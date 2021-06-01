using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RetryQuitButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        Destroy(AudioPlayer.instance.gameObject);
        SceneManager.LoadScene(WormholeController.checkPoint);
        HeroBehavior.Respawn();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
