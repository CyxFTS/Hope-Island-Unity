using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform trans;

    private Vector3 offset;
    void Awake()
    {
        offset = transform.position - trans.position;
    }
    void LateUpdate()
    {
        if(!isShake)
            transform.position = offset + trans.position;
    }
    private bool isShake;
    public void CameraShake(float duration, float strength)
    {
        if (!isShake)
            StartCoroutine(Shake(duration, strength));
    }

    IEnumerator Shake(float duration, float strength)
    {
        isShake = true;
        Transform camera = transform;
        Vector3 startPosition = camera.position;

        while (duration > 0)
        {
            camera.position = Random.insideUnitSphere * strength + startPosition;
            duration -= Time.deltaTime;
            yield return null;
        }
        camera.position = startPosition;
        isShake = false;
    }
}
