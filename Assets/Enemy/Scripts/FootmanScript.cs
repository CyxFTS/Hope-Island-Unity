using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FootmanScript : MonoBehaviour
{

    [Header("============= Object =============")]
    public Animator anim;
    public NavMeshAgent NMA;
    public GameObject player;
    public Transform[] waypoints;
    public GameObject healthSlider;
    public GameObject[] objs;


    [Header("============= Property ============")]
    public int ChasingDistance;
    //[Range(180, 360)]
    //public float chaseAngle;
    public int attackAngle;
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
    bool seeingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        nowstate = -1;
        waypoints_index = 0;
        totalhealth = enemyHealth;
        anim.SetInteger("State", 0);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.GetComponent<Slider>().value = (float)enemyHealth / totalhealth;
        if (!stateCanChange) return;
        distance = Vector3.Distance(transform.position, player.transform.position);
        distancechange();
        statechange();
    }
    void lookatPlayer()
    {
        float orientTransform = transform.position.x;
        float orientTarget = player.transform.position.x;
        Quaternion newRotation;
        float enemyAimSpeed = 5.0f;
        if (orientTransform > orientTarget)
        {
            // Will Give Rotation angle , so that Arrow Points towards that target
            newRotation = Quaternion.LookRotation(player.transform.position - transform.position, -Vector3.left);
        }
        else
        {
            newRotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.left);
        }
        newRotation.x = 0.0f;
        newRotation.z = 0.0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * enemyAimSpeed);

    }
    void distancechange()
   {

        
        if (distance <= AttackDistance) {
            
                nowstate = 2;
            
        }
        else if (distance <= ChasingDistance)
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
            print("State0");
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
            print("State1");

        }
        else if (nowstate == 2)//Attack State
        {
            NMA.stoppingDistance = AttackDistance;
            Vector3 direction = player.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle < 40 && angle > -40) anim.SetInteger("State", 3);
            else {
                anim.SetInteger("State", 0);
                lookatPlayer();
            }
            
            //Debug.DrawLine(transform.position,player.transform.position,Color.red);
        }
        else if (nowstate == -1)
        {
            print("State-1");
            anim.SetInteger("State", 0);
            transform.LookAt(new Vector3(0, -1, 0));
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
        anim.SetInteger("State", 4);
    }
    
    public void finishDamage()
    {
        if (enemyHealth <= 0)
        {
            NMA.isStopped = true;
            anim.SetInteger("State", 5);
        } else
        {
            NMA.isStopped = false;
            anim.SetInteger("State", 1);
            stateCanChange = true;
        }
    }
    public void dead()
    {
        this.gameObject.SetActive(false);
        healthSlider.SetActive(false);
        Vector3 itemLocation = this.transform.position;
        for (int i = 0; i < objs.Length; i++)
        {
            Vector3 randomItemLocation = itemLocation;
            randomItemLocation += new Vector3(Random.Range(-2, 3), 0.2f, Random.Range(-2, 2));
            Instantiate(objs[i], randomItemLocation, objs[i].transform.rotation);
        }

    }
    public void attackPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        if (Vector3.Distance(transform.position, player.transform.position) < AttackDistance + 1&& angle < 30 && angle > -30)
        {
            player.GetComponent<PlayerController>().SetDamage(10);
        }
    }
    public void stopMoving()
    {
       // NMA.isStopped = true;
    }
    public void startMoving()
    {
        //transform.LookAt(player.transform.position);
        //NMA.isStopped = false;
    }
}
