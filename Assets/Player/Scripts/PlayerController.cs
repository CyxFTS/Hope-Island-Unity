using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float walkSpeed;
    private float runSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private Camera Camera;
    public HashSet<int> AttackedEnemies = new HashSet<int>();
    private Animator anim;

    private float horizontal, vertical;

    private CharacterController controller;

    private float attackRate = 1.0f;

    private float nextAttack;

    private bool attacking = false, moving = true, invincible;

    public PlayerStats stats;// = new PlayerStats();

    private float moveSpeed;//, HP, strength, defense;

    private GameObject lockTarget;

    private Sword sword;

    public int BonusId = 0;

    private PlayerSkills mods;// = new PlayerSkills();

    private ProjectileShooting proj;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        proj = GetComponent<ProjectileShooting>();
        mods = GetComponent<PlayerSkills>();
        stats = GetComponent<PlayerStats>();
        moveSpeed = stats.movementSpeed.GetCalculatedStatValue() / 10.0f;
        walkSpeed = moveSpeed / 10f * 5f;
        runSpeed = moveSpeed / 10f * 7f;
        //HP = stats.HP.GetCalculatedStatValue();
        //strength = stats.strength.GetCalculatedStatValue();
        //defense = stats.defense.GetCalculatedStatValue();
        anim = GetComponentInChildren<Animator>();
        sword = GetComponentInChildren<Sword>();
        sword.Damage = 10;
    }

    // Update is called once per frame
    void Update()
    {
        sword.Damage = stats.strength.GetCalculatedStatValue();
        Move();
        //StartAttack();
        if (Input.GetKeyDown(KeyCode.Space) && !attacking)
        {
            //StartCoroutine(StrengthMod(-0.2f, 5f));
            //Debug.Log(BonusId);
            StartCoroutine(Roll());
        }
        UpdateProjectile();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        horizontal = ETCInput.GetAxis("Horizontal");
        vertical = ETCInput.GetAxis("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical);
        Debug.Log(moveDirection);
        //moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
        moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;
        Debug.Log(moveDirection);



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
            //Rotate();
        }
    }
    private void UpdateProjectile()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))//Fire
        {
            proj.currSkill = mods.fireball;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//Arrow
        {
            proj.currSkill = mods.summonArrows;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//Lighting
        {
            proj.currSkill = mods.lightning;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))//Poison
        {
            proj.currSkill = mods.poisonousFumes;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))//Default
        {
            mods.feelNoPain.StartSkill();
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

    public void StartAttack()
    {
        if (/*Input.GetButton("Fire1") &&*/ Time.time > nextAttack && moving)
        {
            nextAttack = Time.time + attackRate;
            StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        if (lockTarget != null)
        {
            transform.LookAt(lockTarget.transform);
        }
        attacking = true;
        sword.GetComponent<Collider>().isTrigger = true;
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");
        
        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        sword.GetComponent<Collider>().isTrigger = false;
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

    public void SetDamage(float power)
    {
        if (invincible)
            return;
        float damage = power;// / stats.defense.GetCalculatedStatValue();
        stats.HP.AddStatBonus(new StatBonus(-damage, BonusId++));
        print(stats.HP.GetCalculatedStatValue());
    }

    public IEnumerator StrengthMod(float scale, float time)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.strength.AddStatMods(b);
        yield return new WaitForSeconds(time);
        stats.strength.RemoveStatMods(b);
    }
    public IEnumerator DefenseMod(float scale, float time)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.defense.AddStatMods(b);
        yield return new WaitForSeconds(time);
        stats.defense.RemoveStatMods(b);
    }
    public IEnumerator MovmentMod(float scale, float time)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.movementSpeed.AddStatMods(b);
        yield return new WaitForSeconds(time);
        stats.movementSpeed.RemoveStatMods(b);
    }
    public IEnumerator HPMod(float additive, float time)
    {
        StatBonus b = new StatBonus(additive, BonusId++);
        stats.HP.AddStatBonus(b);
        yield return new WaitForSeconds(time);
        stats.HP.RemoveStatBonus(b);
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
