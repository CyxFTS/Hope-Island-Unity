using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip potionSound;
    public enum Type // your custom enumeration
    {
        HP,
        Energy,
        Stamina
    };

    public Type type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //audioSource.clip = swordHit;
            //audioSource.Play();
            if(type == Type.HP)
            {
                StartCoroutine(other.GetComponent<PlayerController>().HPMod(1f, 10f));
            }
            if (type == Type.Energy)
            {
                StartCoroutine(other.GetComponent<PlayerController>().EnergyMod(1f, 20f));
            }
            if (type == Type.Stamina)
            {
                StartCoroutine(other.GetComponent<PlayerController>().StaminaMod(1f, 20f));
            }
            Destroy(this.gameObject);
        }
    }
}
