using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    public Transform trans;

    private Vector3 offset;
    void Awake()
    {
        offset = transform.position - trans.position;
    }
    void LateUpdate()
    {
        transform.position = offset + trans.position;
    }
}
