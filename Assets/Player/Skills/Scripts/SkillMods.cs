using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMods
{
    public BasicAttack basicAttack = new BasicAttack();
    public Fireball fireball = new Fireball();
    public SummonArrows summonArrows = new SummonArrows();
    public Lightning lightning = new Lightning();
    public PoisonousFumes poisonousFumes = new PoisonousFumes();
}

enum SkillType
{
    Basic,
    Dot,
    Buff,
    Debuff
}
enum DamageType
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
public class Warcry : BaseSkill
{
}