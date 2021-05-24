using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public GameObject followObject;
    public Vector2 followOffset;
    public float speed = 3f;
    private Vector2 threshold;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start(){
        followOffset.x = 5;

        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector2 follow = followObject.transform.position;
        float xDifference = follow.x - transform.position.x;
        float yDifference = follow.y - transform.position.y;

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x){
            newPosition.x = follow.x;
        }

        float moveSpeed = rb.velocity.x > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.fixedDeltaTime);
    }
    private Vector3 calculateThreshold(){
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        // t.y -= followOffset.y;
        return t;
    }
    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.blue;
    //     Vector2 border = calculateThreshold();
    //     Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    // }
}
