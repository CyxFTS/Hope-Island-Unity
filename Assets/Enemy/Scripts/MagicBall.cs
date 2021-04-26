using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    // private GameObject ball;
    public float liveTime;
    public float mySpeed;
    private float passTime;

    public int damage=10;
    // Start is called before the first frame update
    void Start()
    {
        // ball = GetComponent<GameObject>();
        // Debug.Log("in the ball, the position is"+transform.rotation);
        // transform.Translate(transform.forward*speed*liveTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(passTime>=liveTime){
            // Debug.Log("destroy!!!"+passTime);
            DestroyImmediate(gameObject);
        }else{
            transform.Translate(transform.forward* mySpeed * Time.deltaTime,Space.World);
            // Debug.Log(transform.forward* mySpeed * Time.deltaTime+" delta time:"+Time.deltaTime);
            passTime+=Time.deltaTime;
            // Debug.Log("continue passtime:"+passTime+" "+liveTime+" "+(passTime>=liveTime));
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        // speed = speed * -1;
        // Debug.Log("hello in trigger!"+other);
        
        GameObject gobj = other.gameObject;
        // Debug.Log(other.name+" "+other.gameObject+" tag:"+gobj.tag);
        if(gobj.tag.Equals("Player")){
            // Debug.Log("set damage in the ontrigger!");
            gobj.GetComponent<PlayerController>().SetDamage(damage);
        }
        // if(other.name.Equals("<name>")){
        //     GameObject gameobj = other.gameObject;
        //     gameobj.GetComponent<script>().setDamage(this.damage);
        // }
        // if(other.nam)

    }
}
