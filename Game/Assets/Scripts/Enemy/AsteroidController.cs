using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float maxVerticalSpread = 7.0f;
    public float leftEdge = -35.0f;
    public int maxAsteroids = 20;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i <= maxAsteroids; i++)
        {
            Instantiate(asteroidPrefab, transform.TransformPoint(Random.Range(leftEdge, 0.0f), Random.Range(-maxVerticalSpread, maxVerticalSpread), 0.0f), Quaternion.identity);
        }   
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            new Vector2(transform.position.x, transform.position.y - maxVerticalSpread), 
            new Vector2(transform.position.x, transform.position.y + maxVerticalSpread));

        Gizmos.DrawLine(
            new Vector2(transform.position.x + leftEdge, transform.position.y - maxVerticalSpread), 
            new Vector2(transform.position.x + leftEdge, transform.position.y + maxVerticalSpread));
    }
}
