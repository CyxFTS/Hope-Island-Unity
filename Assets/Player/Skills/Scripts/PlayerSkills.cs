using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public BasicAttack basicAttack = new BasicAttack();
    public Fireball fireball = new Fireball();
    public SummonArrows summonArrows = new SummonArrows();
    public Lightning lightning = new Lightning();
    public PoisonousFumes poisonousFumes = new PoisonousFumes();
    public Warcry warcry = new Warcry();
    public Metallicize metallicize = new Metallicize();
    public Berserk berserk = new Berserk();
    public FeelNoPain feelNoPain = new FeelNoPain();
    public Rushdown rushdown = new Rushdown();
    public Offering offering = new Offering();
    public HealingWave healingWave = new HealingWave();
    public Sprint sprint = new Sprint();
    public Roll roll = new Roll();
    public Invisibility invisibility = new Invisibility();
    public static bool rushDowningActiving = false;
    private static PlayerController controller;
    public GameObject[] Clothes;
    Renderer rend;
    void Start()
    {
        controller = GetComponent<PlayerController>();
        rend = Clothes[1].GetComponent<Renderer>();
        
        rend.materials[1].shader = Shader.Find("ASESampleShaders/RimLight");
    }
    void Update()
    {
        float shininess = Mathf.PingPong(Time.time, 1.0f);
        rend.material.SetFloat("_RimPower", shininess);
        //StartCoroutine(controller.StrengthMod(-0.2f, 5f));
        //Debug.Log(controller.BonusId);
        if (warcry.flag)
        {
            StartCoroutine(controller.StrengthMod(warcry.GetStrengthUpMod() / 100f, warcry.duration));
            StartCoroutine(ChangeRim(Color.red, warcry.duration));
            warcry.flag = false;
        }
        if (metallicize.flag)
        {
            StartCoroutine(controller.StartInvincible(metallicize.duration));
            StartCoroutine(controller.SpeedMod(-metallicize.GetSpeedDownMod() / 100f, metallicize.GetSpeedDownTime()));
            metallicize.flag = false;
        }
        if (berserk.flag)
        {
            StartCoroutine(controller.StrengthMod(berserk.GetStrengthUpMod() / 100f, berserk.duration));
            StartCoroutine(controller.SpeedMod(berserk.GetSpeedUpMod() / 100f, berserk.duration));
            StartCoroutine(controller.HPMod(-berserk.GetHpLostPerSec(), berserk.duration));
            berserk.flag = false;
        }
        if (feelNoPain.flag)
        {
            StartCoroutine(controller.DefenseMod(feelNoPain.GetDefenseUpMod() / 100f, feelNoPain.duration));
            feelNoPain.flag = false;
        }
        if (rushdown.flag)
        {
            //StartCoroutine(controller.SpeedMod(rushdown.GetSpeedUpMod() / 100f, rushdown.GetSpeedUpDuration()));
            StartCoroutine(rushdown.StartRushDown(rushdown.duration));
            rushdown.flag = false;
        }
        if (offering.flag)
        {
            StartCoroutine(controller.EnergyRechargeMod(offering.GetEnergyRechargeMod() / 100f, offering.duration));
            StartCoroutine(controller.StrengthMod(offering.GetStrengthUpMod() / 100f, offering.duration));
            StartCoroutine(controller.HPMod(-offering.GetHpLostPerSec(), offering.duration));
            offering.flag = false;
        }
        if (healingWave.flag)
        {
            StartCoroutine(controller.HPMod(healingWave.GetHpRecoveryMod() , healingWave.duration));
            StartCoroutine(controller.StrengthMod(healingWave.GetStrengthUpMod() / 100f, healingWave.duration));
            healingWave.flag = false;
        }
    }
    private IEnumerator ChangeRim(Color c, float duration)
    {
        
        rend.material.SetColor("_RimColor", c);
        yield return new WaitForSeconds(duration);
        rend.material.SetColor("_RimColor", Color.black);
    }
    public enum SkillType
    {
        Basic,
        Dot,
        Buff,
        Debuff
    }
    public enum DamageType
    {
        Physical,
        Fire,
        Electro,
        Poison
    }
    public enum StaminaType
    {
        Once,
        OverTime
    }
    public class BaseSkill
    {
        public List<float> mod;
        public int type;
        public int skillLevel;
        public string description;
        public virtual void StartSkill()
        {

        }
    }
    public class StaminaSkill : BaseSkill
    {
        public float staminaCost;
        public int staminaType;
        public float GetCurrentMod()
        {
            return mod[skillLevel];
        }

    }
    public class Sprint : StaminaSkill
    {
        public Sprint()
        {
            description = "Sprint";
        }
    }
    public class Roll : StaminaSkill
    {
        public Roll()
        {
            description = "Roll";
        }
    }
    public class Invisibility : StaminaSkill
    {
        public Invisibility()
        {
            description = "Invisibility";
        }
    }
    public class DamageSpell : BaseSkill
    {
        public float energyCost;
        public int damageType;
        public void LevelUp()
        {
            if (skillLevel < 3)
            {
                skillLevel++;
            }
        }
        public void LevelDown()
        {
            if (skillLevel > 3)
            {
                skillLevel--;
            }
        }
        public float GetCurrentMod()
        {
            return mod[skillLevel];
        }
        public override void StartSkill()
        {
            controller.SetStartShooting(controller.input.PlayerMain.EnergySkill1.triggered);
        }

    }
    public class BasicAttack : BaseSkill
    {
        public float energyRecharge;
        public int damageType;
        public BasicAttack()
        {
            mod = new List<float>();
            mod.Add(50.0f);
            type = (int)SkillType.Basic;
            damageType = (int)DamageType.Physical;
            energyRecharge = 10f;
            skillLevel = 0;
            description = "Basic Attack";
        }
    }

    public class Fireball : DamageSpell
    {
        public Fireball()
        {
            mod = new List<float>();
            mod.Add(80f); mod.Add(90f); mod.Add(100f);
            type = (int)SkillType.Basic;
            damageType = (int)DamageType.Fire;
            energyCost = 50;
            skillLevel = 0;
            description = "Fire Ball";
        }
    }
    public class SummonArrows : DamageSpell
    {
        public SummonArrows()
        {
            mod = new List<float>();
            mod.Add(100f); mod.Add(110f); mod.Add(120f);
            type = (int)SkillType.Basic;
            damageType = (int)DamageType.Physical;
            energyCost = 80;
            skillLevel = 0;
            description = "Summon Arrows";
        }
    }
    public class Lightning : DamageSpell
    {
        public Lightning()
        {
            mod = new List<float>();
            mod.Add(60f); mod.Add(65f); mod.Add(70f);
            type = (int)SkillType.Basic;
            damageType = (int)DamageType.Electro;
            energyCost = 50;
            skillLevel = 0;
            description = "Lightning";
        }
    }
    public class PoisonousFumes : DamageSpell
    {
        public float interval, duration;
        public PoisonousFumes()
        {
            mod = new List<float>();
            mod.Add(10f); mod.Add(12f); mod.Add(15f);
            type = (int)SkillType.Dot;
            damageType = (int)DamageType.Poison;
            energyCost = 80;
            skillLevel = 0;
            interval = 2f;
            duration = 8f;
            description = "Poisonous Fumes";
        }
    }
    public class IronMirror : BaseSkill
    {
    }
    /// <summary>
    /// Buff skill
    /// </summary>
    public class BuffSkill : BaseSkill
    {
        public bool flag = false;
        public float duration;
        public float energyCost;
        public void LevelUp()
        {
            if (skillLevel < 3)
            {
                skillLevel++;
            }
        }
        public void LevelDown()
        {
            if (skillLevel > 3)
            {
                skillLevel--;
            }
        }
        public float GetDuration()
        {
            return duration;
        }
        public override void StartSkill()
        {
            flag = true;
        }
    }

    /// <summary>
    /// Warcry: Strength up, Basic Attack deal more damage
    ///   10%/15%/20%, 10%, 5s
    ///   energy cost: 50
    /// </summary>
    public class Warcry : BuffSkill
    {
        public float basicAttackMod = 10f;
        public Warcry()
        {
            mod = new List<float>();
            mod.Add(10f); mod.Add(12f); mod.Add(15f);
            type = (int)SkillType.Buff;
            energyCost = 50;
            skillLevel = 0;
            duration = 5f;
            description = "Warcry";
        }
        public float GetStrengthUpMod()
        {
            return mod[skillLevel];
        }
        public float GetBasicAttackMod()
        {
            return basicAttackMod;
        }

    }
    /// <summary>
    /// Metallicize: invincible, then speed down
    ///     5s, 60%/50%/40%, 10s
    ///     energy cost: 100
    /// </summary>
    public class Metallicize : BuffSkill
    {
        public float speedDownTime = 10f;
        public Metallicize()
        {
            mod = new List<float>();
            mod.Add(60f); mod.Add(50f); mod.Add(40f);
            type = (int)SkillType.Buff;
            energyCost = 100;
            skillLevel = 0;
            duration = 5f;
            description = "Metallicize";
        }
        public float GetSpeedDownMod()
        {
            return mod[skillLevel];
        }
        public float GetSpeedDownTime()
        {
            return speedDownTime;
        }
    }
    /// <summary>
    /// Berserk: Strength & speed up, lose HP over time
    ///     20%/25%/30%, 10%, 2% HP/s, 10s
    ///     energy cost: 60
    /// </summary>
    public class Berserk : BuffSkill
    {
        public float speedUp = 10f;
        public float loseHp = 2f;
        public Berserk()
        {
            mod = new List<float>();
            mod.Add(60f); mod.Add(50f); mod.Add(40f);
            type = (int)SkillType.Buff;
            energyCost = 60;
            skillLevel = 0;
            duration = 10f;
            description = "Berserk";
        }
        public float GetStrengthUpMod()
        {
            return mod[skillLevel];
        }
        public float GetSpeedUpMod()
        {
            return speedUp;
        }
        public float GetHpLostPerSec()
        {
            return loseHp;
        }
    }
    /// <summary>
    /// Feel No Pain: Defense up
    ///     20%/25%/30%, 10s
    ///     energy cost: 50
    /// </summary>
    public class FeelNoPain : BuffSkill
    {
        public FeelNoPain()
        {
            mod = new List<float>();
            mod.Add(20f); mod.Add(25f); mod.Add(30f);
            type = (int)SkillType.Buff;
            energyCost = 50;
            skillLevel = 0;
            duration = 10f;
            description = "FeelNoPain";
        }
        public float GetDefenseUpMod()
        {
            return mod[skillLevel];
        }
    }
    /// <summary>
    /// Rushdown: every time player do basic attack, speed up
    ///     8%, 2s/2.5s/3s, 10s
    ///     energy cost: 30%
    /// </summary>
    public class Rushdown : BuffSkill
    {
        public bool flagSpeed = false;
        public List<float> speedUpDuration;
        public Rushdown()
        {
            mod = new List<float>();
            mod.Add(8f); mod.Add(8f); mod.Add(8f);
            type = (int)SkillType.Buff;
            energyCost = 50;
            skillLevel = 0;
            duration = 10f;
            speedUpDuration = new List<float>();
            speedUpDuration.Add(2); speedUpDuration.Add(2.5f); speedUpDuration.Add(3);
            description = "Rushdown";
        }
        public float GetSpeedUpMod()
        {
            return mod[skillLevel];
        }
        public float GetSpeedUpDuration()
        {
            return speedUpDuration[skillLevel];
        }
        public override void StartSkill()
        {
            flag = true;
            flagSpeed = true;
        }
        public IEnumerator StartRushDown(float duration)
        {
            rushDowningActiving = true;
            yield return new WaitForSeconds(duration);
            rushDowningActiving = false;
        }
    }
    /// <summary>
    /// Offering: lose HP, increase energy recharge & strength
    ///     3% HP/s, 20%/25%/30%, 20%, 5s
    ///     energy cost: 100
    /// </summary>
    public class Offering : BuffSkill
    {
        public float loseHp = 3f;
        public float strengthUpMod = 10f;
        public Offering()
        {
            mod = new List<float>();
            mod.Add(20f); mod.Add(25f); mod.Add(30f);
            type = (int)SkillType.Buff;
            energyCost = 100;
            skillLevel = 0;
            duration = 5f;
            description = "Offering";
        }
        public float GetEnergyRechargeMod()
        {
            return mod[skillLevel];
        }
        public float GetStrengthUpMod()
        {
            return strengthUpMod;
        }
        public float GetHpLostPerSec()
        {
            return loseHp;
        }
    }
    /// <summary>
    /// Healing Wave: Restores HP & strength up
    ///     30%/35%/40%, 10%, 10s
    ///     energy cost: 120
    /// </summary>
    public class HealingWave : BuffSkill
    {
        public float strengthUpMod = 10f;
        public HealingWave()
        {
            mod = new List<float>();
            mod.Add(30f); mod.Add(35f); mod.Add(40f);
            type = (int)SkillType.Buff;
            energyCost = 120;
            skillLevel = 0;
            duration = 10f;
            description = "Healing Wave";
        }
        public float GetHpRecoveryMod()
        {
            return mod[skillLevel];
        }
        public float GetStrengthUpMod()
        {
            return strengthUpMod;
        }
    }

}

