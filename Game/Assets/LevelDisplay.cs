using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelDisplay : MonoBehaviour
{
    public static bool controlDisplay = false;
    public TextMeshProUGUI levelText;

    public GameObject displayKeys;
    public GameObject displayMissile;

    private const float transitionDuration = 0.5f;
    private float timer;
    void Awake()
    {
        levelText.text = SceneManager.GetActiveScene().name;
    }
    // Start is called before the first frame update
    void Start()
    {
        if(!controlDisplay && SceneManager.GetActiveScene().name == "Level 1")
        {
            StartCoroutine(showControl(displayKeys));
            controlDisplay = true;
        }
            
    }

    public void DisplayMissileKey()
    {
        StartCoroutine(showControl(displayMissile));
    }

    IEnumerator showControl(GameObject obj)
    {
        timer = 0f;
        obj.SetActive(true);

        while(timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float scale = Mathf.Lerp(0f, 1f, timer / transitionDuration);
            obj.transform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }        

        yield return new WaitForSeconds(2.0f);

        timer = transitionDuration;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            float scale = Mathf.Lerp(0f, 1f, timer / transitionDuration);
            obj.transform.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }        

        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
