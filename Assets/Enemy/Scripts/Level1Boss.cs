using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Level1Boss : MonoBehaviour
{
    [Header("============= Object =============")]
    public Animator anim;
    public NavMeshAgent NMA;
    public GameObject player;
    public Transform[] waypoints;
    public GameObject[] objs;
    public GameObject healthSlider;


    [Header("============= Property ============")]
    public int ChasingDistance;
    [Range(0, 360)]
    public float chaseAngle;
    [Range(3,5)]
    public float AttackDistance;
    public int runSpeed;
    public int walkSpeed;
    public int enemyHealth;


    int waypoints_index;
    float distance;
    int nowstate;
    bool stateCanChange = true;
    private int totalhealth;
    private ParticleSystem particle;
    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        nowstate = -1;
        waypoints_index = 0;
        totalhealth = enemyHealth;
        particle = this.GetComponentInChildren<ParticleSystem>();
        anim.SetInteger("State", 0);
        particle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stateCanChange) return;
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        // if(Input.GetKey(KeyCode.J)){
        //     setDamage(10);
        //     return;
        // }
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        distancechange(angle);
        print("state:"+nowstate);
        statechange();
    }

     void distancechange(float angle)
   {
        if (distance <= AttackDistance) {
            transform.LookAt(player.transform.position);
            if(enemyHealth<=30)nowstate = 3;
            else nowstate = 2;
        }
        else if (distance <= ChasingDistance && angle < chaseAngle / 2 && angle > -chaseAngle / 2)
        {
            nowstate = 1;
        }
        else
        {
            if (waypoints.Length == 0||(waypoints.Length == 1 && Vector3.Distance(transform.position, waypoints[0].transform.position) < 1))
            {
                nowstate = -1;
            }
            else
            {
                nowstate = 0;
            }
        }
    }

    void statechange()
    {
        NMA.isStopped = false;
        if (nowstate == 0)//Cruise State
        {
            NMA.stoppingDistance = 0;
            anim.SetInteger("State", 1);
            NMA.speed = walkSpeed;
            NMA.SetDestination(waypoints[waypoints_index].position);
            CruisePosition();
          
        }
        else if (nowstate == 1)//Chasing State
        {
            NMA.destination = player.transform.position;
            NMA.speed = runSpeed;
            anim.SetInteger("State", 2);

        }
        else if (nowstate == 2)//Attack State
        {
            NMA.stoppingDistance = AttackDistance;
            anim.SetInteger("State", 3);
            //Debug.DrawLine(transform.position,player.transform.position,Color.red);
        }
        else if(nowstate==3){
            NMA.stoppingDistance = AttackDistance;
            anim.SetInteger("State", 4);
        }
        else if (nowstate == -1)
        {
            anim.SetInteger("State", 0);
            // transform.LookAt(new Vector3(0, -1, 0));
        }
    }

    private void CruisePosition(){
         
      //Debug.DrawLine(transform.position, nextpos, Color.red);
        if (NMA.remainingDistance <= NMA.stoppingDistance)
        {
           //Debug.Log(NMA.remainingDistance + " : " + NMA.stoppingDistance);
            waypoints_index = (waypoints_index + 1) % waypoints.Length;
            NMA.SetDestination(waypoints[waypoints_index].position);
        }
    }
    public void setDamage(int damage)
    {
        stateCanChange = false;
        NMA.isStopped = true;
        enemyHealth -= damage;
        healthSlider.GetComponent<Slider>().value = (float)enemyHealth / totalhealth;
        anim.SetInteger("State", 5);
        Debug.Log(damage);
    }
    
    public void finishDamage()
    {
        if (enemyHealth <= 0)
        {
            NMA.isStopped = true;
            isPlaying = false;
            particle.Stop();
            anim.SetInteger("State", 6);
        } else
        {
            if(enemyHealth<=30&&!isPlaying){
                // print("play the particle!!!");
                particle.Play();
                isPlaying = true;
            }
            NMA.isStopped = false;
            anim.SetInteger("State", 1);
            stateCanChange = true;
        }
    }

    public void dead()
    {
        isPlaying = false;
        // print("dead!!!!");
        particle.Stop();
        this.gameObject.SetActive(false);
        Vector3 itemLocation = this.transform.position;
        for(int i=0;i<objs.Length;i++){
            Vector3 randomItemLocation = itemLocation;
            randomItemLocation += new Vector3(Random.Range(-2,3),0.2f,Random.Range(-2,2));
            Instantiate(objs[i],randomItemLocation,objs[i].transform.rotation);
        }
        
        healthSlider.SetActive(false);
    }
    public void attackPlayer01()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < AttackDistance + 1)
        {
            // player.GetComponent<PlayerController>().SetDamage(10);
        }
    }
    public void attackPlayer02()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < AttackDistance + 1)
        {
            // player.GetComponent<PlayerController>().SetDamage(20);
        }
    }
}
