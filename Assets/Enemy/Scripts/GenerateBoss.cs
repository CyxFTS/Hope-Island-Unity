using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBoss : MonoBehaviour
{
    public int numBoss = 1;
    public Transform generatingPosition;
    public GameObject finalBoss;

    public GameObject player;
    public GameObject healthSlider;

    public Transform[] waypoints;
    public GameObject[] objs;

    public GameObject remoteObjct;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void reduceNum(){
        numBoss--;
        if(numBoss==0){
            GameObject boss = Instantiate(finalBoss,generatingPosition.position,generatingPosition.rotation);
            boss.GetComponent<Level3Boss>().remoteObjct = this.remoteObjct;
            boss.GetComponent<Level3Boss>().player = this.player;
            boss.GetComponent<Level3Boss>().waypoints = this.waypoints;
            boss.GetComponent<Level3Boss>().objs = this.objs;
            boss.GetComponent<Level3Boss>().healthSlider = this.healthSlider;
            healthSlider.SetActive(true);
            healthSlider.GetComponent<SliderScripts>().player = boss;
            
        }
    }
}
