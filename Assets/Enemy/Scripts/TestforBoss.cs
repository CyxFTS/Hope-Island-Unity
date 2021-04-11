using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestforBoss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setDamage(int damage){
        // Debug.Log("get the damage "+damage);
    }
    public void setDamageTask(){
        InvokeRepeating("damageTask",0.1f,1f);
    }

    public void damageTask(){
        setDamage(20);
    }
    public void cancelDamageTask(){
        CancelInvoke();
    }
}
