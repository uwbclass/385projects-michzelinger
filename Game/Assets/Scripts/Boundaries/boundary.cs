using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public Vector2 boundarySize;
    public GameObject top;
    public GameObject bottom;
    public GameObject left;
    public GameObject right;


    public GameObject bossGate;
    public float bossGateLocation;

    void Awake()
    {
        top.transform.localPosition = new Vector2(0,(boundarySize.y + 0.5f));
        bottom.transform.localPosition = new Vector2(0, -(boundarySize.y + 0.5f));
        left.transform.localPosition = new Vector2(-(boundarySize.x + 0.5f), 0);
        right.transform.localPosition = new Vector2((boundarySize.x + 0.5f), 0);

        top.GetComponent<BoxCollider2D>().size = new Vector2(boundarySize.x * 2 + 2, 1);
        bottom.GetComponent<BoxCollider2D>().size = new Vector2(boundarySize.x * 2 + 2, 1);
        left.GetComponent<BoxCollider2D>().size = new Vector2(1, boundarySize.y * 2 + 2);
        right.GetComponent<BoxCollider2D>().size = new Vector2(1, boundarySize.y * 2 + 2);

        bossGate.transform.localPosition = new Vector2(bossGateLocation - 0.5f, 0);
        bossGate.GetComponent<BoxCollider2D>().size = new Vector2(1, boundarySize.y * 2 + 2);
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Vector2 border = boundarySize;
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));

        Gizmos.DrawLine(new Vector2(transform.position.x + bossGateLocation, transform.position.y - border.y), 
                        new Vector2(transform.position.x + bossGateLocation, transform.position.y + border.y));
    }

    public void CloseGate()
    {
        bossGate.SetActive(true);
    }
}
