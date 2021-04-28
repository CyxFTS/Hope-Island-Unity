using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject[] Players;

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

    private float moveSpeed, HP, strength, defense;

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
    public bool loaded = false;
    
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
        audioSource = GetComponent<AudioSource>();
        //energySkill1 = skills.lightning;
        //energySkill2 = skills.warcry;
        //staminaSkill = skills.invisibility;

        LoadPlayerSaveData();
        ApplyPlayerSelection();
        anim = GetComponentInChildren<Animator>();
        animEvent = GetComponentInChildren<PlayerAnimateEvent>();
        sword = GetComponentInChildren<Sword>();
        sword.Damage = 10;
        loaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (died)
            return;

        sword.Damage = stats.strength.GetCalculatedStatValue();
        sword.energyCharge = 5f * stats.EnergyRecharge.GetCalculatedStatValue();
        if (staminaSkill.description == "Locked Talent")
        {
            ((PlayerSkills.LockedTalent)staminaSkill).CheckUnlock();
            if (((PlayerSkills.LockedTalent)staminaSkill).isTalentRealsed && ((PlayerSkills.LockedTalent)staminaSkill).unlocked == false)
            {
                ((PlayerSkills.LockedTalent)staminaSkill).unlocked = true;
                skills.lockedTalentAura.GetComponent<FrontAttack>().playMeshEffect = true;
            }
                
        }
        StartAttack();

        //staminaSkill = skills.invisibility;
        StaminaSkill();

        if (stats.Stamina.GetCalculatedStatValue() < stats.Stamina.BaseValue)
        {
            StartCoroutine(StaminaMod(0.2f * stats.StaminaRecharge.GetCalculatedStatValue(), 0.1f)); ;
        }

        //energySkill1 = skills.lightning;
        //energySkill2 = skills.warcry;
        EnergySkills();
        CheckHP();
        //Debug.Log(stats.HP.GetCalculatedStatValue());
    }
    private void StaminaSkill()
    {
        if (staminaSkill.description == "Roll" && input.PlayerMain.StaminaSkill.triggered && !attacking)
        {
            Roll();
        }
        Move();
        if (staminaSkill.description == "Invisibility")
        {
            if (input.PlayerMain.StaminaSkill.triggered && !attacking)
            {
                if (!((PlayerSkills.Invisibility)staminaSkill).isActive && stats.Stamina.GetCalculatedStatValue() > 0)
                {
                    skills.StartTransperency();
                    ((PlayerSkills.Invisibility)staminaSkill).isActive = true;
                    
                }
                else
                {
                    skills.EndTransperency();
                    ((PlayerSkills.Invisibility)staminaSkill).isActive = false;
                }
            }
            if(((PlayerSkills.Invisibility)staminaSkill).isActive && stats.Stamina.GetCalculatedStatValue() <= 0)
            {
                skills.EndTransperency();
                ((PlayerSkills.Invisibility)staminaSkill).isActive = false;
            }
            if (((PlayerSkills.Invisibility)staminaSkill).isActive)
            {
                StartCoroutine(StaminaMod(-0.5f, 0.1f));
            }
        }
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
        moveDirection *= (stats.movementSpeed.GetCalculatedStatValue() / 100f);
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
        if (staminaSkill.description == "Invisibility" && ((PlayerSkills.Invisibility)staminaSkill).isActive)
        {
            return;
        }
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
            //StartCoroutine(Attack());
        }
    }
    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.35f);
        //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        sword.GetComponent<Collider>().isTrigger = false;
        AttackedEnemies.Clear();
        //attacking = false;
    }
    public void AttackHit()
    {
        if (staminaSkill.description == "Locked Talent")
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
        //print(stats.HP.GetCalculatedStatValue());
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
    public void SetHP(float HP)
    {
        float diff = HP - stats.HP.GetCalculatedStatValue();
        StartCoroutine(HPMod(diff, 0.1f));
    }
    public IEnumerator HPMod(float additive, float duration)
    {
        StatBonus b = new StatBonus(additive, BonusId++);
        
        float t = 0f;
        while(t < duration)
        {
            if (stats.HP.GetCalculatedStatValue() + additive > stats.HP.BaseValue)
            {
                b = new StatBonus(stats.HP.BaseValue - stats.HP.GetCalculatedStatValue(), BonusId++);
                stats.HP.AddStatBonus(b);
                break;
            }
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
            if (stats.Energy.GetCalculatedStatValue() + additive > stats.Energy.BaseValue)
            {
                b = new StatBonus(stats.Energy.BaseValue - stats.Energy.GetCalculatedStatValue(), BonusId++);
                stats.Energy.AddStatBonus(b);
                break;
            }
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
            if (stats.Stamina.GetCalculatedStatValue() + additive > stats.Stamina.BaseValue)
            {
                b = new StatBonus(stats.Stamina.BaseValue - stats.Stamina.GetCalculatedStatValue(), BonusId++);
                stats.Stamina.AddStatBonus(b);
                break;
            }
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
    private string FirstLetterToUpper(string str)
    {
        if (str == null)
            return null;

        if (str.Length > 1)
            return char.ToUpper(str[0]) + str.Substring(1);

        return str.ToUpper();
    }
    public void SavePlayerSaveData()
    {
        string eSkill1 = energySkill1.description;
        string eSkill2 = energySkill2.description;
        string sSkill = staminaSkill.description;
        ES3.Save("energySkill1.description", eSkill1);
        ES3.Save("energySkill2.description", eSkill2);
        ES3.Save("staminaSkill.description", sSkill);
        int eSkill1Lv = energySkill1.skillLevel;
        int eSkill2Lv = energySkill2.skillLevel;
        int sSkillLv = staminaSkill.skillLevel;
        ES3.Save("energySkill1.skillLevel", eSkill1Lv);
        ES3.Save("energySkill2.skillLevel", eSkill2Lv);
        ES3.Save("staminaSkill.skillLevel", sSkillLv);
        var id = skills.PlayerId;
        ES3.Save("PlayerId", id);
        ES3.Save("HP", stats.HP.GetCalculatedStatValue());
    }
    public void LoadPlayerSaveData()
    {
        var regex = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])");

        HP = ES3.Load("HP", 100.0f);
        SetHP(HP);

        string defaultSkill = "Healing Wave";

        string e1 = (string)ES3.Load("energySkill1.description", defaultValue: defaultSkill);
        int e1Lv = ES3.Load("energySkill1.skillLevel", 0);

        string e2 = (string)ES3.Load("energySkill2.description", defaultValue: "Lightning");
        int e2Lv = ES3.Load("energySkill2.skillLevel", 0);

        string s = (string)ES3.Load("staminaSkill.description", defaultValue: "Locked Talent");
        int sLv = ES3.Load("staminaSkill.skillLevel", 0);

        skills.PlayerId = ES3.Load("PlayerId", 2);

        foreach (var prop in skills.GetType().GetFields())
        {
            //Debug.LogFormat("{0}", prop.Name);
            var name = FirstLetterToUpper(regex.Replace(prop.Name, " "));
            if (name == e1)
            {
                var p = (PlayerSkills.BaseSkill)prop.GetValue(skills);
                energySkill1 = p;
                //Debug.Log(p.description);
                energySkill1.skillLevel = e1Lv;
                //Debug.Log(skills.warcry.skillLevel);
                //Debug.Log(p.GetType().GetProperty("description").GetValue(p));
            }
            else if (name == e2)
            {
                var p = (PlayerSkills.BaseSkill)prop.GetValue(skills);
                energySkill2 = p;
                energySkill2.skillLevel = e2Lv;
            }
            else if (name == s)
            {
                var p = (PlayerSkills.BaseSkill)prop.GetValue(skills);
                staminaSkill = p;
                staminaSkill.skillLevel = sLv;
            }
        }
    }
    public void ApplyPlayerSelection()
    {
        if(skills.PlayerId == 0)
        {
            Players[0].SetActive(true);
            Players[1].SetActive(false);
            Players[2].SetActive(false);
        }
        if (skills.PlayerId == 1)
        {
            Players[0].SetActive(false);
            Players[1].SetActive(true);
            Players[2].SetActive(false);
        }
        if (skills.PlayerId == 2)
        {
            Players[0].SetActive(false);
            Players[1].SetActive(false);
            Players[2].SetActive(true);
        }
    }
}
