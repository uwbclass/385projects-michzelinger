using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float duration = 0.2f;
    public float magnitude = 0.3f;
    public void CallShake()
    {
        StartCoroutine(Shake());
    }

    public IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos;
    }
}