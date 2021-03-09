using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonus 
{
    public float BonusValue { get; set; }
    public int id;
    public StatBonus(float bonusValue, int id)
    {
        this.BonusValue = bonusValue;
        this.id = id;
        //Debug.Log("new bonus");
    }
}
