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

    public PlayerSkills.DamageSpell currSkill;
    private PlayerController player;
    private PlayerInput input;

    private void Awake()
    {
        input = new PlayerInput();
    }


    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Start()
    {
        Prefab = 0;
        currSkill = new PlayerSkills.Fireball();
        player = GetComponent<PlayerController>();

    }

    void Update()
    {
        if (currSkill.description == "Fire Ball")//Fire
        {
            Prefab = 1;
        }
        else if (currSkill.description == "Summon Arrows")//Arrow
        {
            Prefab = 2;
        }
        else if (currSkill.description == "Lightning")//Lighting
        {
            Prefab = 3;
        }
        else if (currSkill.description == "Poisonous Fumes")//Poison
        {
            Prefab = 4;
        }
        else//Default
        {
            Prefab = 0;
        }

        //Fast shooting
        if (/*Input.GetMouseButton(1) &&*/ player.GetStartShooting() && fireCountdown <= -0.5f && CostPlayerEnergy())
        {
            Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation).GetComponent<Projectile>().currSkill = this.currSkill;
            Debug.Log(player.stats.Energy.GetCalculatedStatValue());
            
            fireCountdown = 0;
            fireCountdown += hSliderValue;
            player.SetStartShooting(false);
        }
        fireCountdown -= Time.deltaTime;

    }

    bool CostPlayerEnergy()
    {
        if(player.stats.Energy.GetCalculatedStatValue() >= currSkill.energyCost)
        {
            StartCoroutine(player.EnergyMod(-currSkill.energyCost, 0.1f));
            return true;
        }
        return false;
    }
}
