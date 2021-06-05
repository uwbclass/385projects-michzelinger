using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class WormholeController : MonoBehaviour
{
    public static int checkPoint = 1;

    public GameObject[] wormholes;
    public GameObject[] enemies;
    public Animator transitionAnim;

    // Update is called once per frame
    void Start()
    {
        enemies = FindGameObjectsInLayer(8);
    }
    void Update()
    {
        if(enemies.Length > 0)
        {
            enemies = FindGameObjectsInLayer(8);
        }
        else if(enemies.Length == 0)
        {
            for(int i = 0; i < wormholes.Length; i++)
            {
                wormholes[i].SetActive(true);
            }
        }
    }

    public void GameOver()
    {
        StartCoroutine(LoadGameOver());
    }

    IEnumerator LoadGameOver()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        Destroy(AudioPlayer.instance.gameObject);
        SceneManager.LoadScene("DeathScene");
    }

    public void NextScene()
    {
        StartCoroutine(LoadNext());
    }

    IEnumerator LoadNext()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);

        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 4:
                checkPoint = 4;
                Destroy(AudioPlayer.instance.gameObject);
                break;
            case 5:
                checkPoint = 6;
                Destroy(AudioPlayer.instance.gameObject);
                break;
            case 8:
                checkPoint = 8;
                Destroy(AudioPlayer.instance.gameObject);
                break;
            case 9:
                Destroy(AudioPlayer.instance.gameObject);
                break;
            default:
                break;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PreviousScene(int sceneIndex) // How many scenes to go back
    {
        StartCoroutine(LoadPrevious(sceneIndex));
    }
    IEnumerator LoadPrevious(int sceneIndex) 
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneIndex);
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new System.Collections.Generic.List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return goList.ToArray();
        }
        return goList.ToArray();
    }
}
