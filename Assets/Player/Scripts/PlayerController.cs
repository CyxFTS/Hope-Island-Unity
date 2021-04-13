using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private float attackRate = 0.7f;

    private float nextAttack;

    private bool attacking = false, moving = true, invincible, died = false, dodging = false;

    public PlayerStats stats;// = new PlayerStats();

    private float moveSpeed;//, HP, strength, defense;

    private GameObject lockTarget;

    public Sword sword;

    public int BonusId = 0;

    private PlayerSkills skills;// = new PlayerSkills();

    //public GameObject healthSlider;

    private AudioSource audioSource;

    public AudioClip deathSound;

    public PlayerInput input;

    public bool firstAttack = true;
    public bool desireToCombo = false;
    private bool startShooting = false;

    private PlayerAnimateEvent animEvent;

    public PlayerSkills.BaseSkill energySkill1, energySkill2, staminaSkill;
    public int CrescendoCnt = 0;
    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        skills = GetComponent<PlayerSkills>();
        stats = GetComponent<PlayerStats>();
        moveSpeed = stats.movementSpeed.GetCalculatedStatValue() / 10.0f;
        walkSpeed = moveSpeed / 10f * 5f;
        runSpeed = moveSpeed / 10f * 7f;
        //HP = stats.HP.GetCalculatedStatValue();
        //strength = stats.strength.GetCalculatedStatValue();
        //defense = stats.defense.GetCalculatedStatValue();
        anim = GetComponentInChildren<Animator>();
        animEvent = GetComponentInChildren<PlayerAnimateEvent>();
        sword = GetComponentInChildren<Sword>();
        sword.Damage = 10;
        audioSource = GetComponent<AudioSource>();
        energySkill1 = skills.fireball;
        energySkill2 = skills.lightning;
        staminaSkill = skills.sprint;
    }

    // Update is called once per frame
    void Update()
    {
        if (died)
            return;

        sword.Damage = stats.strength.GetCalculatedStatValue();
        StartAttack();

        staminaSkill = skills.sprint;
        StaminaSkill();

        if (stats.Stamina.GetCalculatedStatValue() < stats.Stamina.BaseValue)
        {
            StartCoroutine(StaminaMod(0.2f * stats.StaminaRecharge.GetCalculatedStatValue(), 0.1f)); ;
        }

        energySkill1 = skills.lightning;
        energySkill2 = skills.metallicize;
        EnergySkills();
        CheckHP();
    }
    private void StaminaSkill()
    {
        if (staminaSkill.description == "Roll" && input.PlayerMain.StaminaSkill.triggered && !attacking)
        {
            Roll();
        }
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        horizontal = input.PlayerMain.Move.ReadValue<Vector2>().x;
        vertical = input.PlayerMain.Move.ReadValue<Vector2>().y;
        moveDirection = new Vector3(horizontal, 0f, vertical);
        //Debug.Log(moveDirection);
        //moveDirection = Quaternion.AngleAxis(90, Vector3.up) * moveDirection;
        moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;
        //transform.rotation = Quaternion.Euler(0, 45, 0) * transform.rotation;
        //Debug.Log(moveDirection);

        bool running = false;
        if (staminaSkill.description == "Sprint")
            running = (input.PlayerMain.StaminaSkill.activeControl != null && stats.Stamina.GetCalculatedStatValue() > 0) ? true : false;
    
        if (moveDirection != Vector3.zero && !running/* && !Input.GetKey(KeyCode.LeftShift)*/)
        {
            //Walk
            Walk();
        }
        else if (moveDirection != Vector3.zero && running/* && Input.GetKey(KeyCode.LeftShift)*/)
        {
            //Run
            StartCoroutine(StaminaMod(-0.5f, 0.1f));
            Run();
        }
        else if (moveDirection == Vector3.zero)
        {
            //Idle
            Idle();
        }

        

        moveDirection *= moveSpeed;

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        LockUnlock();
        
        if(moveDirection != Vector3.zero && !attacking)
        {
            Rotate();
        }
        
    }
    private void EnergySkills()
    {
        if (input.PlayerMain.EnergySkill1.triggered)
        {
            energySkill1.StartSkill();
            if (staminaSkill.description == "Crescendo")
            {
                staminaSkill.StartSkill();
            }
            if (energySkill1.energyCost >= 60.0f && staminaSkill.description == "Riposte")
            {
                staminaSkill.StartSkill();
            }
        }
            
        if (input.PlayerMain.EnergySkill2.triggered)
        {
            energySkill2.StartSkill();
            if (staminaSkill.description == "Crescendo")
            {
                staminaSkill.StartSkill();
            }
            if (energySkill2.energyCost >= 60.0f && staminaSkill.description == "Riposte")
            {
                staminaSkill.StartSkill();
            }
        }
            
        //Debug.Log(stats.defense.GetCalculatedStatValue());
    }
    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.AngleAxis(45, Vector3.up) * Quaternion.Euler(0, -45, 0) * moveDirection), rotationSpeed * Time.deltaTime);
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

    private void CheckHP()
    {
        if(stats.HP.GetCalculatedStatValue() <= 0 && died == false )
        {
            died = true;
            moving = false;
            StartCoroutine(Died());
        }
    }
    private IEnumerator Died()
    {
        audioSource.clip = deathSound;
        audioSource.Play();
        anim.SetTrigger("Died");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("death");
        //UIManager.Instance.PopUpWnd(PathCollections.UI_DeathPrompt);
        this.gameObject.SetActive(false);
    }

    public void StartAttack()
    {
        if (input.PlayerMain.Attack.triggered /*&& Time.time > nextAttack*/ && moving)
        {
            nextAttack = Time.time + attackRate;
            if (lockTarget != null)
            {
                transform.LookAt(lockTarget.transform);
            }
            attacking = true;
            sword.GetComponent<Collider>().isTrigger = true;
            anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);

            if (firstAttack && !desireToCombo)
            {
                firstAttack = false;
                desireToCombo = false;
                anim.SetTrigger("Attack");
                //role.animComponent.animator.CrossFade("AttackCombo", 0.2f, 0, 0);
            }
            else
            {
                if (animEvent.canCombo)
                {
                    desireToCombo = true;
                    firstAttack = false;
                }
            }
            StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.0f);
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        sword.GetComponent<Collider>().isTrigger = false;
        AttackedEnemies.Clear();
        //attacking = false;
    }
    public void AttackHit()
    {
        if (staminaSkill.description == "LockedTalent")
        {
            ((PlayerSkills.LockedTalent)staminaSkill).UnlockOnce();
        }
    }

    public void setAttacking(bool a)
    {
        attacking = a;
    }

    private void Roll()
    {
        moving = false;
        anim.SetTrigger("Roll");
        if(!invincible)
            StartCoroutine(StartInvincible(0.9f));
        moving = true;
        StartCoroutine(StaminaMod(10f, 0.1f));
    }

    public void SetDamage(float power)
    {
        if (invincible)
            return;
        if(dodging)
        {
            ((PlayerSkills.Riposte)staminaSkill).Consume();
            return;
        }
        float damage = power / stats.defense.GetCalculatedStatValue();
        stats.HP.AddStatBonus(new StatBonus(-damage, BonusId++));
        print(stats.HP.GetCalculatedStatValue());
    }
    public IEnumerator StartInvincible(float duration)
    {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }
    public IEnumerator StrengthMod(float scale, float duration)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.strength.AddStatMods(b);
        yield return new WaitForSeconds(duration);
        stats.strength.RemoveStatMods(b);
    }
    public IEnumerator DefenseMod(float scale, float duration)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.defense.AddStatMods(b);
        yield return new WaitForSeconds(duration);
        stats.defense.RemoveStatMods(b);
    }
    public IEnumerator SpeedMod(float scale, float duration)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.movementSpeed.AddStatMods(b);
        yield return new WaitForSeconds(duration);
        stats.movementSpeed.RemoveStatMods(b);
    }
    public IEnumerator HPMod(float additive, float duration)
    {
        StatBonus b = new StatBonus(additive, BonusId++);
        
        float t = 0f;
        while(t < duration)
        {
            stats.HP.AddStatBonus(b);
            t += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator EnergyMod(float additive, float duration)
    {
        StatBonus b = new StatBonus(additive, BonusId++);
        float t = 0f;
        while (t < duration)
        {
            stats.Energy.AddStatBonus(b);
            t += 1;
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator EnergyRechargeMod(float scale, float duration)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.EnergyRecharge.AddStatMods(b);
        yield return new WaitForSeconds(duration);
        stats.EnergyRecharge.RemoveStatMods(b);
    }
    public IEnumerator StaminaRechargeMod(float scale, float duration)
    {
        StatBonus b = new StatBonus(scale, BonusId++);
        stats.StaminaRecharge.AddStatMods(b);
        yield return new WaitForSeconds(duration);
        stats.StaminaRecharge.RemoveStatMods(b);
    }
    public IEnumerator StaminaMod(float additive, float duration)
    {
        StatBonus b = new StatBonus(additive, BonusId++);
        float t = 0f;
        while (t < duration)
        {
            stats.Stamina.AddStatBonus(b);
            t += 1;
            yield return new WaitForSeconds(1f);
        }
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
    public bool SetStartShooting(bool s)
    {
        startShooting = s;
        return startShooting;
    }
    public bool GetStartShooting()
    {
        return startShooting;
    }
    public void SetDodging(bool d)
    {
        dodging = d;
    }
}
