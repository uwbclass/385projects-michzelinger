using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    [Header("Camera Follow")]
    private GameObject followObject;
    private float followOffset = 7.5f;
    public float speed = 3f;
    public GameObject cam;
    private float threshold;
    private Rigidbody2D rb;
    private float camOrthoWidth;
    private Vector2 camPosLimit;

    [Header("Camera Shake")]
    public float duration = 0.1f;
    public float magnitude = 0.1f;

    [Header("Boundary")]
    public GameObject boundary;




    [Header("Boss Level")]
    public bool bossLevel = false;
    public float transitionTime = 2f;
    public GameObject boss;
    private bool following = true;
    private Vector3 originalPos;

    // Start is called before the first frame update
    void Start(){
        followObject = HeroBehavior.instance.gameObject;
        
        Rect aspect = Camera.main.pixelRect;
        camOrthoWidth = Camera.main.orthographicSize * aspect.width / aspect.height;
        threshold = camOrthoWidth - followOffset;

        camPosLimit = CalculateCamPosLimit();

        rb = followObject.GetComponent<Rigidbody2D>();

        originalPos = boss.transform.position;
        boss.transform.position = (boss.transform.position + new Vector3(5.0f, 0f, 0f));
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector2 follow = followObject.transform.position;

        if(following)
        {
            float xDifference = follow.x - cam.transform.position.x;

            Vector3 newPosition = cam.transform.position;
            if(Mathf.Abs(xDifference) >= threshold){
                // newPosition.x = follow.x;
                newPosition.x = Mathf.Clamp(follow.x, camPosLimit.x, camPosLimit.y);
            }

            float moveSpeed = rb.velocity.x > speed ? rb.velocity.x : speed;
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, newPosition, moveSpeed * Time.fixedDeltaTime);
        }

        if(bossLevel && following && follow.x > (boundary.transform.position.x + boundary.GetComponent<Boundary>().bossGateLocation + 0.5f))
        {
            following = false;
            boundary.GetComponent<Boundary>().CloseGate();
            StartCoroutine(FocusBoss());
        }
    }
    
    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.blue;
    //     Vector2 border = calculateThreshold();
    //     Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    // }

    // Returns the limit of the camera positions in the form of (min, max)
    private Vector2 CalculateCamPosLimit()
    {
        float min = boundary.transform.localPosition.x - boundary.GetComponent<Boundary>().boundarySize.x + camOrthoWidth;
        float max = boundary.transform.localPosition.x + boundary.GetComponent<Boundary>().boundarySize.x - camOrthoWidth;
        return new Vector2(min, max);
    }


    public void CallShake()
    {
        StartCoroutine(Shake());
    }

    public IEnumerator Shake()
    {
        Vector3 originalPos = cam.transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cam.transform.localPosition = new Vector3(
                cam.transform.localPosition.x + x, 
                cam.transform.localPosition.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        cam.transform.localPosition = originalPos;
    }

    IEnumerator FocusBoss()
    {
        HeroBehavior.instance.canMove = false;
        float timeElapsed = 0f;
        Vector3 camOrigPos = cam.transform.position;
        Vector3 finalPos = new Vector3(camPosLimit.y, 0, cam.transform.position.z);
        while(timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;
            cam.transform.position = Vector3.Lerp(camOrigPos, finalPos, timeElapsed / transitionTime);
            yield return null;
        }
        cam.transform.position = finalPos;
        
        timeElapsed = 0;
        while(timeElapsed < transitionTime)
        {
            timeElapsed += Time.deltaTime;
            boss.transform.position = Vector3.Lerp(originalPos + Vector3.right * 5, originalPos, timeElapsed / transitionTime);
            yield return null;
        }
        boss.transform.position = originalPos;


        HeroBehavior.instance.canMove = true;
    }
}