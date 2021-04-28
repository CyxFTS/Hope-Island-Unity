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
    
    public Crescendo crescendo = new Crescendo();
    public Riposte riposte = new Riposte();
    public LockedTalent lockedTalent = new LockedTalent();
    public CursedBlood cursedBlood = new CursedBlood();

    public static bool rushDowningActiving = false;
    private static PlayerController controller;
    //public GameObject[] Clothes;
    public Material Invis;
    public Material[] BodyMat;
    public GameObject[] Bodies;
    Renderer cloth;

    Renderer body, head, hair;

    public int PlayerId = 1;

    [Header("============= Damage Spell =============")]
    public Sprite fireballIcon;
    public Sprite summonArrowsIcon;
    public Sprite lightningIcon;
    public Sprite poisonousFumesIcon;

    [Header("============= Power =============")]
    public Sprite warcryIcon;
    public Sprite metallicizeIcon;
    public Sprite berserkIcon;
    public Sprite feelNoPainIcon;
    public Sprite rushdownIcon;
    public Sprite offeringIcon;
    public Sprite healingWaveIcon;

    [Header("============= Stamina Skill =============")]
    public Sprite sprintIcon;
    public Sprite rollIcon;
    public Sprite invisibilityIcon;

    [Header("============= Ability =============")]
    public Sprite crescendoIcon;
    public Sprite riposteIcon;
    public Sprite lockedTalentIcon;
    public Sprite cursedBloodIcon;

    [Header("============= Power =============")]
    public GameObject warcryAura;
    public GameObject metallicizeAura;
    public GameObject berserkAura;
    public GameObject feelNoPainAura;
    public GameObject rushdownAura;
    public GameObject offeringAura;
    public GameObject healingWaveAura;

    [Header("============= Ability =============")]
    public GameObject crescendoAura;
    public GameObject riposteAura;
    public GameObject lockedTalentAura;
    public GameObject cursedBloodAura;

    private static ProjectileShooting proj;
    void Start()
    {
        controller = GetComponent<PlayerController>();
        proj = controller.GetComponent<ProjectileShooting>();

        fireball.icon = fireballIcon;
        summonArrows.icon = summonArrowsIcon;
        lightning.icon = lightningIcon;
        poisonousFumes.icon = poisonousFumesIcon;

        warcry.icon = warcryIcon;
        metallicize.icon = metallicizeIcon;
        berserk.icon = berserkIcon;
        feelNoPain.icon = feelNoPainIcon;
        rushdown.icon = rushdownIcon;
        offering.icon = offeringIcon;
        healingWave.icon = healingWaveIcon;

        sprint.icon = sprintIcon;
        roll.icon = rollIcon;
        invisibility.icon = invisibilityIcon;

        crescendo.icon = crescendoIcon;
        riposte.icon = riposteIcon;
        lockedTalent.icon = lockedTalentIcon;
        cursedBlood.icon = cursedBloodIcon;
    }
    void Update()
    {
        cloth = Bodies[PlayerId * 3].GetComponent<Renderer>();

        body = Bodies[PlayerId * 3].GetComponent<Renderer>();
        hair = Bodies[PlayerId * 3 + 1].GetComponent<Renderer>();
        head = Bodies[PlayerId * 3 + 2].GetComponent<Renderer>();

        float shininess = Mathf.PingPong(Time.time, 1.0f);
        cloth.materials[0].SetFloat("_RimPower", shininess);
        //StartCoroutine(controller.StrengthMod(-0.2f, 5f));
        //Debug.Log(controller.BonusId);
        if (warcry.flag)
        {
            StartCoroutine(controller.StrengthMod(warcry.GetStrengthUpMod() / 100f, warcry.duration));
            StartCoroutine(ChangeRim(Color.red, warcry.duration));
            //StartCoroutine(SetTransperency(warcry.duration));
            CostPlayerEnergy(warcry);
            warcry.flag = false;
        }
        if (metallicize.flag)
        {
            //Debug.Log(controller.BonusId);
            StartCoroutine(controller.StartInvincible(metallicize.duration));
            StartCoroutine(controller.SpeedMod(-metallicize.GetSpeedDownMod() / 100f, metallicize.GetSpeedDownTime()));
            StartCoroutine(ChangeRim(Color.white, metallicize.duration));
            CostPlayerEnergy(metallicize);
            metallicize.flag = false;
        }
        if (berserk.flag)
        {
            StartCoroutine(controller.StrengthMod(berserk.GetStrengthUpMod() / 100f, berserk.duration));
            StartCoroutine(controller.SpeedMod(berserk.GetSpeedUpMod() / 100f, berserk.duration));
            StartCoroutine(controller.HPMod(-berserk.GetHpLostPerSec(), berserk.duration));
            StartCoroutine(SetAura(berserkAura, berserk.duration));
            CostPlayerEnergy(berserk);
            berserk.flag = false;
        }
        if (feelNoPain.flag)
        {
            StartCoroutine(controller.DefenseMod(feelNoPain.GetDefenseUpMod() / 100f, feelNoPain.duration));
            StartCoroutine(SetAura(feelNoPainAura, feelNoPain.duration));
            CostPlayerEnergy(feelNoPain);
            feelNoPain.flag = false;
        }
        if (rushdown.flag)
        {
            StartCoroutine(controller.SpeedMod(rushdown.GetSpeedUpMod() / 100f, rushdown.GetSpeedUpDuration()));
//            StartCoroutine(rushdown.StartRushDown(rushdown.duration));
            StartCoroutine(SetAura(rushdownAura, rushdown.duration));
            CostPlayerEnergy(rushdown);
            rushdown.flag = false;
        }
        if (offering.flag)
        {
            StartCoroutine(controller.EnergyRechargeMod(offering.GetRechargeMod() / 100f, offering.duration));
            StartCoroutine(controller.StrengthMod(offering.GetStrengthUpMod() / 100f, offering.duration));
            StartCoroutine(controller.HPMod(-offering.GetHpLostPerSec(), offering.duration));
            StartCoroutine(SetAura(offeringAura, offering.duration));
            CostPlayerEnergy(offering);
            offering.flag = false;
        }
        if (healingWave.flag)
        {
            StartCoroutine(controller.HPMod(healingWave.GetHpRecoveryMod() , 0.1f));
            StartCoroutine(controller.StrengthMod(healingWave.GetStrengthUpMod() / 100f, healingWave.duration));
            StartCoroutine(SetAura(healingWaveAura, healingWave.duration));
            CostPlayerEnergy(healingWave);
            healingWave.flag = false;
        }
        if (controller.staminaSkill.description == "Crescendo")
        {
            //crescendo.SetLevel(controller.CrescendoCnt);
            if (crescendo.isCrescendo())
            {
                crescendo.Consume();
                controller.energySkill1.inCrescendo = true;
                controller.energySkill2.inCrescendo = true;
                StartAura(crescendoAura);
            }
            if(!controller.energySkill1.inCrescendo || !controller.energySkill2.inCrescendo)
            {
                EndAura(crescendoAura);
            }
        }
        //if (controller.staminaSkill.description == "Locked Talent")
        //{
        //    lockedTalent.CheckUnlock();
        //    lockedTalentAura.GetComponent<FrontAttack>().playMeshEffect = true;
        //}
        if (controller.staminaSkill.description == "Cursed Blood")
        {
            cursedBlood.Check();
            StartAura(cursedBloodAura);
        }
        else
        {
            EndAura(cursedBloodAura);
        }
    }
    private IEnumerator ChangeRim(Color c, float duration)
    {
        Shader temp = cloth.materials[0].shader;
        cloth.materials[0].shader = Shader.Find("ASESampleShaders/RimLight");
        cloth.materials[0].SetColor("_RimColor", c);
        yield return new WaitForSeconds(duration);
        cloth.materials[0].SetColor("_RimColor", Color.black);
        cloth.materials[0].shader = temp;
    }
    Material clothTemp;
    Material bodyTemp;
    Material hairTemp;
    Material headTemp;
    private IEnumerator SetTransperency(float duration)
    {
        StartTransperency();
        yield return new WaitForSeconds(duration);
        EndTransperency();
    }
    public void StartTransperency()
    {
        bodyTemp = BodyMat[2];
        body.materials[1].shader = Invis.shader;
        body.materials[1].CopyPropertiesFromMaterial(Invis);

        clothTemp = cloth.material;
        cloth.material = Invis;

        hairTemp = hair.material;
        hair.material = Invis;

        headTemp = head.material;
        head.material = Invis;
    }
    public void EndTransperency()
    {
        body.materials[1].shader = bodyTemp.shader;
        body.materials[1].CopyPropertiesFromMaterial(bodyTemp);

        cloth.material = clothTemp;

        hair.material = hairTemp;

        head.material = headTemp;
    }
    public IEnumerator SetAura(GameObject Aura, float duration)
    {
        StartAura(Aura);
        yield return new WaitForSeconds(duration);
        EndAura(Aura);
    }
    public void StartAura(GameObject Aura)
    {
        Aura.SetActive(true);
    }
    public void EndAura(GameObject Aura)
    {
        Aura.SetActive(false);
    }
    bool CostPlayerEnergy(BaseSkill skill)
    {
        if (controller.stats.Energy.GetCalculatedStatValue() >= skill.energyCost)
        {
            StartCoroutine(controller.EnergyMod(-skill.energyCost, 0.1f));
            return true;
        }
        return false;
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
        public bool inCrescendo = false;
        public Sprite icon;
        public virtual float energyCost { get; set; }
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
        public bool isActive = false;
        public Invisibility()
        {
            description = "Invisibility";
        }
    }
    public class DamageSpell : BaseSkill
    {
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
        public void CheckCrescendo()
        {
            if (inCrescendo)
            {
                for (int i = 0; i < mod.Count; i++)
                {
                    mod[i] *= 1.5f;
                }
            }
        }
        public void RemoveCrescendo()
        {
            if (inCrescendo)
            {
                inCrescendo = false;
                for (int i = 0; i < mod.Count; i++)
                {
                    mod[i] /= 1.5f;
                }
            }
        }
        public override void StartSkill()
        {
            CheckCrescendo();
            proj.currSkill = this;
            if(controller.input.PlayerMain.EnergySkill1.triggered && controller.stats.Energy.GetCalculatedStatValue() - energyCost >= 0)
                controller.SetStartShooting(controller.input.PlayerMain.EnergySkill1.triggered);
            if (controller.input.PlayerMain.EnergySkill2.triggered && controller.stats.Energy.GetCalculatedStatValue() - energyCost >= 0)
                controller.SetStartShooting(controller.input.PlayerMain.EnergySkill2.triggered);
            RemoveCrescendo();
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
            description = "Fireball";
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
            energyCost = 30;
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
        public void CheckCrescendo()
        {
            if (inCrescendo)
            {
                for (int i = 0; i < mod.Count; i++)
                {
                    mod[i] *= 1.5f;
                }
            }
        }
        public void RemoveCrescendo()
        {
            if (inCrescendo)
            {
                inCrescendo = false;
                for (int i = 0; i < mod.Count; i++)
                {
                    mod[i] /= 1.5f;
                }
            }
        }
        public override void StartSkill()
        {
            if (controller.stats.Energy.GetCalculatedStatValue() < energyCost)
                return;
            CheckCrescendo();
            flag = true;
            RemoveCrescendo();
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
            description = "Feel No Pain";
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
            mod.Add(30f); mod.Add(30f); mod.Add(30f);
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
            base.StartSkill();
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
    /// Offering: lose HP, increase energy and stamina recharge & strength
    ///     3% HP/s, 20%/25%/30%, 20%, 5s
    ///     energy cost: 50
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
            energyCost = 50;
            skillLevel = 0;
            duration = 5f;
            description = "Offering";
        }
        public float GetRechargeMod()
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
    ///     energy cost: 100
    /// </summary>
    public class HealingWave : BuffSkill
    {
        public float strengthUpMod = 10f;
        public HealingWave()
        {
            mod = new List<float>();
            mod.Add(30f); mod.Add(35f); mod.Add(40f);
            type = (int)SkillType.Buff;
            energyCost = 100;
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
    public class Crescendo : BaseSkill
    {
        private const int maxLvl = 2;
        private int curLvl = 0;
        public Crescendo()
        {
            mod = new List<float>();
            mod.Add(50.0f);
            description = "Crescendo";
        }
        public override void StartSkill()
        {
            if (curLvl <= maxLvl)
                curLvl++;
        }
        public void SetLevel(int l)
        {
            curLvl = l;
        }
        public void Consume()
        {
            curLvl = 0;
        }
        public bool isCrescendo()
        {
            return curLvl == maxLvl;
        }
    }
    public class Riposte : BaseSkill
    {
        private const int maxStk = 3;
        private int curStk = 0;
        public Riposte()
        {
            mod = new List<float>();
            mod.Add(60.0f);
            description = "Riposte";
        }
        public override void StartSkill()
        {
            if (curStk < maxStk)
                curStk++;
            //Debug.Log(curStk);
            controller.SetDodging(true);
        }
        public void SetLevel(int l)
        {
            curStk = l;
        }
        public void Consume()
        {
            if(curStk > 0)
                curStk--;
            if(curStk == 0)
                controller.SetDodging(false);
            //Debug.Log(curStk);
        }
        public bool isCrescendo()
        {
            return curStk == maxStk;
        }
    }
    public class LockedTalent : BaseSkill
    {
        private int hitRemain = 10;
        public bool isTalentRealsed = false;
        public bool unlocked = false;
        public LockedTalent()
        {
            mod = new List<float>();
            mod.Add(0.3f);
            description = "Locked Talent";
        }
        public void UnlockOnce()
        {
            if(hitRemain > 0)
                hitRemain--;
            if (hitRemain == 0)
                isTalentRealsed = true;
        }
        public void CheckUnlock()
        {
            if(!isTalentRealsed)
            {
                controller.sword.Damage = 0;
            }
            if (isTalentRealsed && !unlocked)
            {
                controller.sword.Damage = controller.stats.strength.GetCalculatedStatValue(); 
                controller.stats.strength.BaseValue *= 1f+mod[0];
                controller.stats.defense.BaseValue *= 1f+mod[0];
                controller.stats.movementSpeed.BaseValue *= 1f+mod[0];
            }
        }
    }
    public class CursedBlood : BaseSkill
    {
        private float strengthUp = 0.3f;
        private float defenseDown = 0.5f;
        private float HPThreshold = 0.3f;
        private bool isActivated = false;
        public CursedBlood()
        {
            description = "Cursed Blood";
        }
        public void Check()
        {
            float ratio = (float)controller.stats.HP.GetCalculatedStatValue() / controller.stats.HP.BaseValue;
            if (ratio >= HPThreshold && !isActivated)
            {
                controller.stats.strength.BaseValue *= (1f + strengthUp);
                controller.stats.defense.BaseValue *= (1f - defenseDown);
                isActivated = true;
            }
            if(ratio < HPThreshold && isActivated)
            {
                controller.stats.strength.BaseValue /= (1f + strengthUp);
                controller.stats.defense.BaseValue /= (1f - defenseDown);
                isActivated = false;
            }
        }
    }
}

