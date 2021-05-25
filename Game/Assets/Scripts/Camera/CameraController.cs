using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    [Header("Camera Follow")]
    public GameObject followObject;
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


    // Start is called before the first frame update
    void Start(){

        Rect aspect = Camera.main.pixelRect;
        camOrthoWidth = Camera.main.orthographicSize * aspect.width / aspect.height;
        threshold = camOrthoWidth - followOffset;

        camPosLimit = CalculateCamPosLimit();

        rb = followObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector2 follow = followObject.transform.position;
        float xDifference = follow.x - cam.transform.position.x;

        Vector3 newPosition = cam.transform.position;
        if(Mathf.Abs(xDifference) >= threshold){
            // newPosition.x = follow.x;
            newPosition.x = Mathf.Clamp(follow.x, camPosLimit.x, camPosLimit.y);
        }

        float moveSpeed = rb.velocity.x > speed ? rb.velocity.x : speed;
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, newPosition, moveSpeed * Time.fixedDeltaTime);
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
}
