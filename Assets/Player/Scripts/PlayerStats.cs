using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public BaseStat HP = new BaseStat(100, "HP", "Your HP");
    public BaseStat Energy = new BaseStat(100, "Energy", "Your Energy");
    public BaseStat EnergyRecharge = new BaseStat(1, "EnergyRecharge", "Your energy recharge rate");
    public BaseStat Stamina = new BaseStat(100, "Stamina", "Your Stamina");
    public BaseStat StaminaRecharge = new BaseStat(1, "StaminaRecharge", "Your stamina recharge rate");
    public BaseStat movementSpeed = new BaseStat(100, "Movement speed", "Your movement speed");
    public BaseStat strength = new BaseStat(10, "Strength", "Your strength");
    public BaseStat defense = new BaseStat(2, "Defense", "Your defense");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
