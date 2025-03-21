﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip potionSound;

    private bool used = false;
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
        if (other.tag == "Player" && !used)
        {
            //audioSource.clip = swordHit;
            //audioSource.Play();

            used = true;

            if(type == Type.HP)
            {
                if(other.GetComponent<PlayerController>().stats.HP.GetCalculatedStatValue() + 10 > other.GetComponent<PlayerController>().stats.HP.BaseValue)
                {
                    var diff = Mathf.Abs(other.GetComponent<PlayerController>().stats.HP.GetCalculatedStatValue() - other.GetComponent<PlayerController>().stats.HP.BaseValue);
                    StartCoroutine(other.GetComponent<PlayerController>().HPMod(-diff, 0.5f));
                }
                else
                    StartCoroutine(other.GetComponent<PlayerController>().HPMod(1f, 10f));
                StartCoroutine(DelayedDestroy(10f));
            }
            if (type == Type.Energy)
            {
                if (other.GetComponent<PlayerController>().stats.Energy.GetCalculatedStatValue() + 20 > other.GetComponent<PlayerController>().stats.Energy.BaseValue)
                {
                    var diff = other.GetComponent<PlayerController>().stats.Energy.GetCalculatedStatValue() - other.GetComponent<PlayerController>().stats.Energy.BaseValue;
                    //Debug.Log(other.GetComponent<PlayerController>().stats.Energy.GetCalculatedStatValue());
                    StartCoroutine(other.GetComponent<PlayerController>().EnergyMod(-diff, 0.5f));
                    //Debug.Log(other.GetComponent<PlayerController>().stats.Energy.GetCalculatedStatValue());

                }
                else
                    StartCoroutine(other.GetComponent<PlayerController>().EnergyMod(1f, 20f));
                StartCoroutine(DelayedDestroy(20f));
            }
            if (type == Type.Stamina)
            {
                if (other.GetComponent<PlayerController>().stats.Stamina.GetCalculatedStatValue() + 20 > other.GetComponent<PlayerController>().stats.Stamina.BaseValue)
                {
                    var diff = Mathf.Abs(other.GetComponent<PlayerController>().stats.Stamina.GetCalculatedStatValue() - other.GetComponent<PlayerController>().stats.Stamina.BaseValue);
                    StartCoroutine(other.GetComponent<PlayerController>().StaminaMod(-diff, 0.5f));
                }
                else
                    StartCoroutine(other.GetComponent<PlayerController>().StaminaMod(1f, 20f));
                StartCoroutine(DelayedDestroy(20f));
            }
            //
        }
    }
    private IEnumerator DelayedDestroy(float time)
    {
        this.gameObject.GetComponentsInChildren<MeshRenderer>()[0].enabled = false;
        this.gameObject.GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
        this.gameObject.GetComponentsInChildren<MeshRenderer>()[2].enabled = false;
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
