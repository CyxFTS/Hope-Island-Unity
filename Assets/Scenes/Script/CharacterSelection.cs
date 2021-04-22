using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class CharacterSelection : MonoBehaviour
{
    public GameObject player;
    public GameObject characterSelection;
    public GameObject skillSelection;
    
    public void SelectCharacterOne() // Vincent
    {
        player.GetComponent<PlayerSkills>().PlayerId = 2;
        GetSkills();
        characterSelection.SetActive(false);
        skillSelection.SetActive(true);
    }
    
    public void SelectCharacterTwo() // Charlotte
    {
        player.GetComponent<PlayerSkills>().PlayerId = 0;
        GetSkills();
        characterSelection.SetActive(false);
        skillSelection.SetActive(true);
    }
    
    public void SelectCharacterThree() // Marc
    {
        player.GetComponent<PlayerSkills>().PlayerId = 1;
        GetSkills();
        characterSelection.SetActive(false);
        skillSelection.SetActive(true);
    }

    private void GetSkills()
    {
        var random = new System.Random();
        if (player.GetComponent<PlayerSkills>().PlayerId == 0) // Charlotte
        {
            var random1 = random.Next(0, 7);
            var random2 = random.Next(0, 7);
            while (random2 == random1)
            {
                random2 = random.Next(0, 7);
            }
            SetCharlotteEnergySkill(1, random1);
            SetCharlotteEnergySkill(2, random2);
            var random3 = random.Next(0, 3);
            SetCharlotteStaminaSkill(random3);
        }
        else if (player.GetComponent<PlayerSkills>().PlayerId == 1) // Marc
        {
            var random1 = random.Next(0, 5);
            var random2 = random.Next(0, 5);
            while (random2 == random1)
            {
                random2 = random.Next(0, 5);
            }
            SetMarcEnergySkill(1, random1);
            SetMarcEnergySkill(2, random2);
            var random3 = random.Next(0, 2);
            SetMarcStaminaSkill(random3);
        }
        else if (player.GetComponent<PlayerSkills>().PlayerId == 2) // Vincent
        {
            var random1 = random.Next(0, 5);
            var random2 = random.Next(0, 5);
            while (random2 == random1)
            {
                random2 = random.Next(0, 5);
            }
            SetVincentEnergySkill(1, random1);
            SetVincentEnergySkill(2, random2);
            var random3 = random.Next(0, 7);
            SetVincentStaminaSkill(random3);
        }
    }

    private void SetCharlotteEnergySkill(int skillNum, int skillID)
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().fireball;
        switch (skillID)
        {
            case 0:
                skill = player.GetComponent<PlayerSkills>().fireball;
                break;
            case 1:
                skill = player.GetComponent<PlayerSkills>().summonArrows;
                break;
            case 2:
                skill = player.GetComponent<PlayerSkills>().lightning;
                break;
            case 3:
                skill = player.GetComponent<PlayerSkills>().poisonousFumes;
                break;
            case 4:
                skill = player.GetComponent<PlayerSkills>().metallicize;
                break;
            case 5:
                skill = player.GetComponent<PlayerSkills>().feelNoPain;
                break;
            case 6:
                skill = player.GetComponent<PlayerSkills>().healingWave;
                break;
        }
        
        switch (skillNum)
        {
            case 1:
                player.GetComponent<PlayerController>().energySkill1 = skill;
                break;
            case 2:
                player.GetComponent<PlayerController>().energySkill2 = skill;
                break;
        }
    }

    private void SetMarcEnergySkill(int skillNum, int skillID)
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().warcry;
        switch (skillID)
        {
            case 0:
                skill = player.GetComponent<PlayerSkills>().warcry;
                break;
            case 1:
                skill = player.GetComponent<PlayerSkills>().berserk;
                break;
            case 2:
                skill = player.GetComponent<PlayerSkills>().feelNoPain;
                break;
            case 3:
                skill = player.GetComponent<PlayerSkills>().rushdown;
                break;
            case 4:
                skill = player.GetComponent<PlayerSkills>().offering;
                break;
        }
        switch (skillNum)
        {
            case 1:
                player.GetComponent<PlayerController>().energySkill1 = skill;
                break;
            case 2:
                player.GetComponent<PlayerController>().energySkill2 = skill;
                break;
        }
    }
    
    private void SetVincentEnergySkill(int skillNum, int skillID)
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().warcry;
        switch (skillID)
        {
            case 0:
                skill = player.GetComponent<PlayerSkills>().fireball;
                break;
            case 1:
                skill = player.GetComponent<PlayerSkills>().lightning;
                break;
            case 2:
                skill = player.GetComponent<PlayerSkills>().rushdown;
                break;
            case 3:
                skill = player.GetComponent<PlayerSkills>().offering;
                break;
            case 4:
                skill = player.GetComponent<PlayerSkills>().healingWave;
                break;
        }
        switch (skillNum)
        {
            case 1:
                player.GetComponent<PlayerController>().energySkill1 = skill;
                break;
            case 2:
                player.GetComponent<PlayerController>().energySkill2 = skill;
                break;
        }
    }

    private void SetCharlotteStaminaSkill(int skillID)
    {
        switch (skillID)
        {
            case 0:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().sprint;
                break;
            case 1:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().roll;
                break;
            case 2:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().invisibility;
                break;
        }
    }
    
    private void SetMarcStaminaSkill(int skillID)
    {
        switch (skillID)
        {
            case 0:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().sprint;
                break;
            case 1:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().roll;
                break;
        }
    }
    
    private void SetVincentStaminaSkill(int skillID)
    {
        switch (skillID)
        {
            case 0:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().sprint;
                break;
            case 1:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().roll;
                break;
            case 2:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().invisibility;
                break;
            case 3:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().crescendo;
                break;
            case 4:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().riposte;
                break;
            case 5:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().lockedTalent;
                break;
            case 6:
                player.GetComponent<PlayerController>().staminaSkill = player.GetComponent<PlayerSkills>().cursedBlood;
                break;
        }
    }
}
