﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public GameObject damageCircle;
    private ParticleSystem circle;
    private float time = 20f;

    public GameObject player;
    public bool taskStart=false;

    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
        circle = damageCircle.GetComponent<ParticleSystem>();
        circle.Stop();
        taskStart = false;
        // var main = circle.main;
        // main.duration = time;
        
        
    }
    void OnParticleTrigger()
    {
        //this function for detecting any collider enters into the circle.
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();

        // get
        int numEnter = circle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = circle.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        int numInside = circle.GetTriggerParticles(ParticleSystemTriggerEventType.Inside,inside);

        if(!taskStart){
            for (int i = 0; i < numEnter; i++)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = new Color32(255, 0, 0, 255);
                enter[i] = p;
                taskStart = true;
                setDamageTask();
                // Debug.Log("SomeOne enter!");
            }
            for (int i = 0; i < numInside; i++)
            {
                ParticleSystem.Particle p = inside[i];
                p.startColor = new Color32(0, 255, 0, 255);
                inside[i] = p;
                taskStart = true;
                setDamageTask();
                // Debug.Log("SomeOne in!");
            }
        }else{
            for (int i = 0; i < numExit; i++)
            {
                ParticleSystem.Particle p = exit[i];
                p.startColor = new Color32(0, 255, 0, 255);
                exit[i] = p;
                taskStart = false;
                cancelDamageTask();
                // Debug.Log("SomeOne exit!");
            }
        }
        

        // set
        // circle.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        // circle.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }

    // Update is called once per frame
    void Update()
    {
        if(taskStart){
            circle.Play();
            time = circle.duration;
            
            Destroy(damageCircle, time);
        }
        // Debug.Log("paritcle System!");
    }
    void OnDestroy()
    {
        cancelDamageTask();
    }
     public void setDamageTask(){
        InvokeRepeating("damageTask",0.5f,0.5f);
    }

    public void damageTask(){
        player.GetComponent<PlayerController>().SetDamage(damage);
    }
    public void cancelDamageTask(){
        CancelInvoke();
    }
}
