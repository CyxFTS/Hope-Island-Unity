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
        if (Input.GetKeyDown(KeyCode.Alpha1))//Fire
        {
            Prefab = 1;
            Prefabs[Prefab].GetComponent<Projectile>().Damage = 22;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//Arrow
        {
            Prefab = 2;
            Prefabs[Prefab].GetComponent<Projectile>().Damage = 22;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//Lighting
        {
            Prefab = 3;
            Prefabs[Prefab].GetComponent<Projectile>().Damage = 22;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))//Poison
        {
            Prefab = 4;
            Prefabs[Prefab].GetComponent<Projectile>().Damage = 22;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))//Default
        {
            Prefab = 0;
            Prefabs[Prefab].GetComponent<Projectile>().Damage = 22;
        }
        //Fast shooting
        if (Input.GetMouseButton(1) && fireCountdown <= -0.5f)
        {
            Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
            fireCountdown = 0;
            fireCountdown += hSliderValue;
        }
        fireCountdown -= Time.deltaTime;

    }
}
