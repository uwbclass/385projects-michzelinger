using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitBehavior : MonoBehaviour
{
    public float OrbitRadius = 2.0f;
    public float OrbitSpeed = 360.0f / 2f; //10 seconds per cycle
    public Transform HostXform;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = HostXform.position + OrbitRadius * transform.right;
        Quaternion r = Quaternion.AngleAxis((OrbitSpeed * Time.smoothDeltaTime), Vector3.forward);
        transform.rotation = r * transform.rotation;
        while(OrbitRadius != 0f)
        {
            OrbitRadius = OrbitRadius - 0.1f;
        }
    }
}
