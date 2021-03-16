using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public List<StatBonus> BaseAdditives { get; set; }
    public List<StatBonus> BaseMods { get; set; }
    public float BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public float FinalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDescription)
    {
        BaseAdditives = new List<StatBonus>();
        BaseMods = new List<StatBonus>();
        BaseValue = baseValue;
        StatName = statName;
        StatDescription = statDescription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        BaseAdditives.Remove(statBonus);
    }

    public void AddStatMods(StatBonus statBonus)
    {
        BaseMods.Add(statBonus);
    }

    public void RemoveStatMods(StatBonus statBonus)
    {
        BaseMods.Remove(statBonus);
    }

    public float GetCalculatedStatValue()
    {
        FinalValue = 0;
        BaseAdditives.ForEach(x=>FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        BaseMods.ForEach(x => FinalValue *= (1 + x.BonusValue));
        return FinalValue;
    }
}
