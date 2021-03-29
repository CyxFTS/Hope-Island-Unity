using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [Header("补偿速度")]
    public float lightSpeed;
    public float heavySpeed;
    [Header("打击感")]
    public float shakeTime;
    public int lightPause;
    public float lightStrength;
    public int heavyPause;
    public float heavyStrength;
    [SerializeField] private Camera _camera;
    public float Damage { get; set; }
    private AudioSource audioSource;

    public AudioClip swordHit;
    public float energyCharge = 10f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && !GetComponentInParent<PlayerController>().AttackedEnemies.Contains(other.GetInstanceID()))
        {
            audioSource.clip = swordHit;
            audioSource.Play();
            GetComponentInParent<PlayerController>().AttackedEnemies.Add(other.GetInstanceID());
            StartCoroutine(GetComponentInParent<PlayerController>().EnergyMod(10f, 0.1f));

            other.GetComponent<FootmanScript>().setDamage((int)Damage);
            AttackSense.Instance.HitPause(lightPause);
            _camera.GetComponent<CameraController>().CameraShake(shakeTime, lightStrength);
        }
        if (other.tag == "Boss" && !GetComponentInParent<PlayerController>().AttackedEnemies.Contains(other.GetInstanceID()))
        {
            audioSource.clip = swordHit;
            audioSource.Play();
            GetComponentInParent<PlayerController>().AttackedEnemies.Add(other.GetInstanceID());
            StartCoroutine(GetComponentInParent<PlayerController>().EnergyMod(10f, 0.1f));

            other.GetComponent<Level1Boss>().setDamage((int)Damage);
            AttackSense.Instance.HitPause(lightPause);
            _camera.GetComponent<CameraController>().CameraShake(shakeTime, lightStrength);
        }
    }
}
