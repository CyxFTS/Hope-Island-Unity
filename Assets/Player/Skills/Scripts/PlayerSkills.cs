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
    private static PlayerController controller;
    void Start()
    {
        controller = GetComponent<PlayerController>();
    }
    void Update()
    {
        //StartCoroutine(controller.StrengthMod(-0.2f, 5f));
        //Debug.Log(controller.BonusId);
        if(feelNoPain.flag)
        {
            StartCoroutine(controller.DefenseMod(0.2f, 5f));
            feelNoPain.flag = false;
        }
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
    public class BaseSkill
    {
        public List<float> mod;
        public int type;
        public int skillLevel;
        public string description;
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
        public void StartSkill()
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
        public new void StartSkill()
        {
            flag = true;
            flagSpeed = true;
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

