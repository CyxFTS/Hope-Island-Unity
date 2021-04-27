using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class SkillSelection : MonoBehaviour
{
    public GameObject player;
    public int currentLevel;
    public Image energySkillOneImage;
    public Image energySkillTwoImage;
    public Image staminaSkillImage;
    public Image newSkillImage;
    private PlayerSkills.BaseSkill _newSkill;
    
    private void Start()
    {
        energySkillOneImage.sprite = player.GetComponent<PlayerController>().energySkill1.icon;
        energySkillTwoImage.sprite = player.GetComponent<PlayerController>().energySkill2.icon;
        staminaSkillImage.sprite = player.GetComponent<PlayerController>().staminaSkill.icon;
        _newSkill = GetNewSkill();
        var random = new System.Random();
        var random1 = random.Next(0, 10);
        _newSkill.skillLevel = random1 > 6 ? 1 : 0;
        newSkillImage.sprite = _newSkill.icon;
    }

    public void UpgradeEnergySkills()
    {
        if (player.GetComponent<PlayerController>().energySkill1.skillLevel < 2)
        {
            player.GetComponent<PlayerController>().energySkill1.skillLevel++;
        }
        if (player.GetComponent<PlayerController>().energySkill2.skillLevel < 2)
        {
            player.GetComponent<PlayerController>().energySkill2.skillLevel++;
        }
        LoadNextScene();
    }

    public void ReplaceSkill()
    {
        if (_newSkill.description == "Fireball" || _newSkill.description == "Summon Arrows" ||
            _newSkill.description == "Lightning" || _newSkill.description == "Poisonous Fumes" ||
            _newSkill.description == "Warcry" || _newSkill.description == "Metallicize" ||
            _newSkill.description == "Berserk" || _newSkill.description == "Feel No Pain" ||
            _newSkill.description == "Rushdown" || _newSkill.description == "Offering" ||
            _newSkill.description == "Healing Wave") // switch energy skill
        {
            var random = new System.Random();
            var skillIdx = random.Next(0, 2);
            var random2 = random.Next(0, 10);
            switch (skillIdx)
            {
                case 0:
                    player.GetComponent<PlayerController>().energySkill1 = _newSkill;
                    if (random2 > 6) player.GetComponent<PlayerController>().energySkill1.skillLevel = 1;
                    break;
                case 1:
                    player.GetComponent<PlayerController>().energySkill2 = _newSkill;
                    if (random2 > 6) player.GetComponent<PlayerController>().energySkill2.skillLevel = 1;
                    break;
            }
        }
        else
        {
            player.GetComponent<PlayerController>().staminaSkill = _newSkill;
        }
        LoadNextScene();
    }

    private PlayerSkills.BaseSkill GetNewSkill()
    {
        var random = new System.Random();
        var skillKind = random.Next(0, 2); // Determine the new skill type (energy or stamina)
        var newSkill = player.GetComponent<PlayerController>().energySkill1;
        switch (player.GetComponent<PlayerSkills>().PlayerId)
        {
            case 0:
                if (skillKind == 0)
                {
                    newSkill = GetCharlotteEnergySkill();
                    while (newSkill.description == player.GetComponent<PlayerController>().energySkill1.description ||
                           newSkill.description == player.GetComponent<PlayerController>().energySkill2.description)
                    {
                        newSkill = GetCharlotteEnergySkill();
                    }
                }
                else
                {
                    newSkill = GetCharlotteStaminaSkill();
                    while (newSkill.description == player.GetComponent<PlayerController>().staminaSkill.description)
                    {
                        newSkill = GetCharlotteStaminaSkill();
                    }
                }

                break;
            case 1:
                if (skillKind == 0)
                {
                    newSkill = GetMarcEnergySkill();
                    while (newSkill.description == player.GetComponent<PlayerController>().energySkill1.description ||
                           newSkill.description == player.GetComponent<PlayerController>().energySkill2.description)
                    {
                        newSkill = GetMarcEnergySkill();
                    }
                }
                else
                {
                    newSkill = GetMarcStaminaSkill();
                    while (newSkill.description == player.GetComponent<PlayerController>().staminaSkill.description)
                    {
                        newSkill = GetMarcStaminaSkill();
                    }
                }
                break;
            case 2:
                if (skillKind == 0)
                {
                    newSkill = GetVincentEnergySkill();
                    while (newSkill.description == player.GetComponent<PlayerController>().energySkill1.description ||
                           newSkill.description == player.GetComponent<PlayerController>().energySkill2.description)
                    {
                        newSkill = GetVincentEnergySkill();
                    }
                }
                else
                {
                    newSkill = GetVincentStaminaSkill();
                    while (newSkill.description == player.GetComponent<PlayerController>().staminaSkill.description)
                    {
                        newSkill = GetVincentStaminaSkill();
                    }
                }
                break;
        }
        return newSkill;
    }

    private PlayerSkills.BaseSkill GetCharlotteEnergySkill()
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().fireball;
        var random = new System.Random();
        var skillID = random.Next(0, 7);
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
        
        return skill;
    }

    private PlayerSkills.BaseSkill GetCharlotteStaminaSkill()
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().fireball;
        var random = new System.Random();
        var skillID = random.Next(0, 3);
        switch (skillID)
        {
            case 0:
                skill = player.GetComponent<PlayerSkills>().sprint;
                break;
            case 1:
                skill = player.GetComponent<PlayerSkills>().roll;
                break;
            case 2:
                skill = player.GetComponent<PlayerSkills>().invisibility;
                break;
        }

        return skill;
    }
    
    private PlayerSkills.BaseSkill GetMarcEnergySkill()
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().fireball;
        var random = new System.Random();
        var skillID = random.Next(0, 5);
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
        
        return skill;
    }
    
    private PlayerSkills.BaseSkill GetMarcStaminaSkill()
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().fireball;
        var random = new System.Random();
        var skillID = random.Next(0, 2);
        switch (skillID)
        {
            case 0:
                skill = player.GetComponent<PlayerSkills>().sprint;
                break;
            case 1:
                skill = player.GetComponent<PlayerSkills>().roll;
                break;
        }

        return skill;
    }
    
    private PlayerSkills.BaseSkill GetVincentEnergySkill()
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().fireball;
        var random = new System.Random();
        var skillID = random.Next(0, 5);
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
        
        return skill;
    }
    
    private PlayerSkills.BaseSkill GetVincentStaminaSkill()
    {
        PlayerSkills.BaseSkill skill = player.GetComponent<PlayerSkills>().sprint;
        var random = new System.Random();
        var skillID = random.Next(0, 7);
        switch (skillID)
        {
            case 0:
                skill = player.GetComponent<PlayerSkills>().sprint;
                break;
            case 1:
                skill = player.GetComponent<PlayerSkills>().roll;
                break;
            case 2:
                skill = player.GetComponent<PlayerSkills>().invisibility;
                break;
            case 3:
                skill = player.GetComponent<PlayerSkills>().crescendo;
                break;
            case 4:
                skill = player.GetComponent<PlayerSkills>().riposte;
                break;
            case 5:
                skill = player.GetComponent<PlayerSkills>().lockedTalent;
                break;
            case 6:
                skill = player.GetComponent<PlayerSkills>().cursedBlood;
                break;
        }

        return skill;
    }
    
    private void LoadNextScene()
    {
        // var random = new System.Random();
            // var randomScene = random.Next(1, 5);
            // var sceneToLoad = "level" + nextLevel + "scene1-" + randomScene;
            String sceneToLoad = "";
            switch (currentLevel)
            {
                case 0:
                    sceneToLoad = "level0Part2";
                    break;
                case 1:
                    sceneToLoad = "Part3";
                    break;
                case 2:
                    sceneToLoad = "level2Part3";
                    break;
                case 3:
                    sceneToLoad = "level3Part3";
                    break;
            }
            player.GetComponent<PlayerController>().SavePlayerSaveData();
            SceneManager.LoadScene(sceneToLoad);
    }
}
