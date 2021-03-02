using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public List<StatBonus> BaseAdditives { get; set; }
    public float BaseValue { get; set; }
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public float FinalValue { get; set; }

    public BaseStat(int baseValue, string statName, string statDescription)
    {
        this.BaseAdditives = new List<StatBonus>();
        this.BaseValue = baseValue;
        this.StatName = statName;
        this.StatDescription = statDescription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        this.BaseAdditives.Remove(statBonus);
    }

    public float GetCalculatedStatValue()
    {
        FinalValue = 0;
        this.BaseAdditives.ForEach(x=>this.FinalValue += x.BonusValue);
        FinalValue += BaseValue;
        return FinalValue;
    }
}
