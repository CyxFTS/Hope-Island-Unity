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

    public DamageSpell currSkill;
    void Start()
    {
        Prefab = 0;
        currSkill = new Fireball();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//Fire
        {
            Prefab = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//Arrow
        {
            Prefab = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//Lighting
        {
            Prefab = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))//Poison
        {
            Prefab = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))//Default
        {
            Prefab = 0;
        }
        
        //Fast shooting
        if (Input.GetMouseButton(1) && fireCountdown <= -0.5f)
        {
            Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation).GetComponent<Projectile>().currSkill = this.currSkill;

            fireCountdown = 0;
            fireCountdown += hSliderValue;
        }
        fireCountdown -= Time.deltaTime;

    }
}
