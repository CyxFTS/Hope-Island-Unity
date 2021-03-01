using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooting : MonoBehaviour
{
    public GameObject FirePoint;
    public Camera Cam;
    public float MaxLength;
    public GameObject[] Prefabs;

    public int Damage = 12;

    private int Prefab;
    private GameObject Instance;
    private float hSliderValue = 0.1f;
    private float fireCountdown = 0f;

    void Start()
    {
        Prefab = 0;
    }

    void Update()
    {
        //Fast shooting
        if (Input.GetMouseButton(1) && fireCountdown <= -0.5f)
        {
            Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation).GetComponent<ProjectileCollision>().Damage = Damage;
            fireCountdown = 0;
            fireCountdown += hSliderValue;
        }
        fireCountdown -= Time.deltaTime;

    }
}
