using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimateEvent : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip swordSwing;

    public bool canCombo;

    public GameObject player;

    private PlayerController controller;
    private Animator anim;

    public GameObject[] Prefabs;
    private int Prefab;
    private GameObject Instance;
    public GameObject SlashPoint;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = player.GetComponent<PlayerController>();
        anim = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySwordSwing()
    {
        audioSource.clip = swordSwing;
        audioSource.Play();
        Counter(0);
    }
    public void SetSlash(int slash)
    {
        Prefab = slash;
    }

    public void CanAttack()
    {
        controller.firstAttack = true;
        controller.setAttacking(false);
    }

    public void CanCombo(int value)
    {
        canCombo = value == 1;
    }

    public void Combo()
    {
        // Debug.LogError("Combo");
        if (controller.desireToCombo)
        {
            controller.desireToCombo = false;
            anim.SetTrigger("Combo");
        }
    }
    public void ClearEnemies()
    {
        controller.sword.GetComponent<Collider>().isTrigger = false;
        controller.AttackedEnemies.Clear();
    }

    private ParticleSystem[] particleSystems = new ParticleSystem[0];
    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > Prefabs.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = Instantiate(Prefabs[Prefab]);
        Instance.transform.SetParent(SlashPoint.transform, false); 
        particleSystems = Instance.GetComponentsInChildren<ParticleSystem>(); //Get color from current instance 
    }
}
