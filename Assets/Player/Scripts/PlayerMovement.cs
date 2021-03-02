using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject Sword;
    public HashSet<int> AttackedEnemies = new HashSet<int>();
    private Animator anim;

    private float horizontal, vertical;

    private CharacterController controller;

    private float attackRate = 1.0f;

    private float nextAttack;

    private bool attacking = false, moving = true, invincible;

    private PlayerStats stats = new PlayerStats();

    public float moveSpeed, HP, strength, defense;

    private GameObject lockTarget;

    public Sword sword;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveSpeed = stats.movementSpeed.GetCalculatedStatValue() / 10.0f;
        HP = stats.HP.GetCalculatedStatValue();
        strength = stats.strength.GetCalculatedStatValue();
        defense = stats.defense.GetCalculatedStatValue();
        anim = GetComponentInChildren<Animator>();
        sword = GetComponentInChildren<Sword>();
        sword.Damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextAttack && moving)
        {
            nextAttack = Time.time + attackRate;
            StartCoroutine(Attack());
        }
        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            StartCoroutine(Roll());
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical);

        moveDirection = Quaternion.AngleAxis(45, Vector3.up) * moveDirection;

        

        float velocityX = Vector3.Dot(moveDirection.normalized, transform.right);
        float velocityZ = Vector3.Dot(moveDirection.normalized, transform.forward);

        //_animator.SetFloat("VelocityZ", Mathf.Abs(velocityZ), 0.1f, Time.deltaTime);
        //_animator.SetFloat("VelocityX", Mathf.Abs(velocityZ), 0.1f, Time.deltaTime);


        if (true)//isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //Walk
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Run
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                //Idle
                Idle();
            }

            moveDirection *= moveSpeed;
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        LockUnlock();
        
        if(!attacking)
        {
            Rotate();
        }
    }

    private void Rotate()
    {

        if (horizontal > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.right), rotationSpeed * Time.deltaTime);
        else if (horizontal < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.left), rotationSpeed * Time.deltaTime);
        if (vertical > 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.forward), 0.67f * rotationSpeed * Time.deltaTime);
        else if (vertical < 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Vector3.back), 0.67f * rotationSpeed * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Run Blend", 0.0f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Run Blend", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Run Blend", 1.0f, 0.1f, Time.deltaTime);
    }

    private IEnumerator Attack()
    {
        if (lockTarget != null)
        {
            transform.LookAt(lockTarget.transform);
        }
        attacking = true;
        Sword.GetComponent<Collider>().isTrigger = true;
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        Sword.GetComponent<Collider>().isTrigger = false;
        AttackedEnemies.Clear();
        attacking = false;
    }

    private IEnumerator Roll()
    {
        moving = false;
        anim.SetTrigger("Roll");
        invincible = true;
        yield return new WaitForSeconds(0.9f);
        moving = true;
        invincible = false;
    }

    public void setDamage(float power)
    {
        if (invincible)
            return;
        float damage = power;// / stats.defense.GetCalculatedStatValue();
        stats.HP.AddStatBonus(new StatBonus(-damage));
        print(stats.HP.GetCalculatedStatValue());
    }

    public void LockUnlock()
    {
        Vector3 tempPosition = transform.position;
        Vector3 center = tempPosition  + transform.forward * -1.5f;

        Collider[] col = Physics.OverlapBox(center, new Vector3(3.0f, 2.0f, 3.0f), Quaternion.identity, LayerMask.GetMask("Enemy"));
        
        foreach (var item in col)
        {
            lockTarget = item.gameObject;
            //Debug.Log(LayerMask.LayerToName(lockTarget.layer));
            break;
        }
        if(col.Length == 0)
        {
            lockTarget = null;
        }
    }
}
