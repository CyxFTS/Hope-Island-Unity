using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public BaseStat HP = new BaseStat(100, "HP", "Your HP");
    public BaseStat movementSpeed = new BaseStat(100, "Movement speed", "Your movement speed");
    public BaseStat strength = new BaseStat(10, "Strength", "Your strength");
    public BaseStat defense = new BaseStat(100, "Defense", "Your defense");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
