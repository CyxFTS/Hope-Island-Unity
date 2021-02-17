using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootmanScript : MonoBehaviour
{

    [Header("============= Object =============")]
    public Animator anim;
    public NavMeshAgent NMA;
    public GameObject player;
    public Transform[] waypoints;


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

    // Start is called before the first frame update
    void Start()
    {
        nowstate = -1;
        waypoints_index = 0;
    }

    // Update is called once per frame
    void Update()
    {   if (!stateCanChange) return;
        distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        distancechange(angle);
        statechange();
    }

     void distancechange(float angle)
   {
        if (distance <= AttackDistance) {
            transform.LookAt(player.transform.position);
            nowstate = 2;
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
        print(nowstate);
        NMA.isStopped = false;
        if (nowstate == 0)//Cruise State
        {
            NMA.stoppingDistance = 0;
            anim.SetBool("walk", true);
            anim.SetBool("attack", false);
            anim.SetBool("run", false);
            anim.SetBool("idle", false);
            NMA.speed = walkSpeed;
            NMA.SetDestination(waypoints[waypoints_index].position);
            CruisePosition();
          
        }
        else if (nowstate == 1)//Chasing State
        {
            NMA.destination = player.transform.position;
            NMA.speed = runSpeed;
            anim.SetBool("run", true);
            anim.SetBool("walk", false);
            anim.SetBool("attack", false);
            anim.SetBool("idle", false);

        }
        else if (nowstate == 2)//Attack State
        {
            NMA.stoppingDistance = AttackDistance;
            anim.SetBool("attack", true);
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
            anim.SetBool("idle", false);
            //Debug.DrawLine(transform.position,player.transform.position,Color.red);
        }
        else if (nowstate == -1)
        {
            anim.SetBool("idle", true);
            anim.SetBool("walk", false);
            anim.SetBool("attack", false);
            anim.SetBool("run", false);
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
        anim.SetBool("damage", true);
        anim.SetBool("idle", false);
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
        anim.SetBool("run", false);
        anim.SetBool("dead", false);
    }
    
    public void finishDamage()
    {
        if (enemyHealth <= 0)
        {
            NMA.isStopped = true;
            anim.SetBool("dead", true);
        } else
        {
            NMA.isStopped = false;
            anim.SetBool("walk", true);
            anim.SetBool("idle", false);
            anim.SetBool("attack", false);
            anim.SetBool("run", false);
            anim.SetBool("damage", false);
            anim.SetBool("dead", false);
            stateCanChange = true;
        }
    }
    public void dead()
    {
        this.gameObject.SetActive(false);
    }
    public void attackPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < AttackDistance + 1)
        {
            print("Attack");
            //API for player
        }
    }
}
