using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerSlider : MonoBehaviour
{

    public GameObject healthSlider;
    public GameObject energySlider;
    public GameObject staminaSlider;
    public GameObject energyButton1;
    public GameObject energyButton2;
    public GameObject staminaButton;
    public GameObject player;
    private PlayerController controller;
    bool loadSkillIcon = true;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider.GetComponent<Slider>().value = 1;
        energySlider.GetComponent<Slider>().value = 1;
        staminaSlider.GetComponent<Slider>().value = 1;
        controller = player.GetComponentInChildren<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (loadSkillIcon && controller.loaded)
        {
            energyButton1.GetComponent<Image>().sprite = controller.energySkill1.icon;
            energyButton2.GetComponent<Image>().sprite = controller.energySkill2.icon;
            staminaButton.GetComponent<Image>().sprite = controller.staminaSkill.icon;
            loadSkillIcon = false;
        }
        healthSlider.GetComponent<Slider>().value = (float)controller.stats.HP.GetCalculatedStatValue() / controller.stats.HP.BaseValue;
        energySlider.GetComponent<Slider>().value = (float)controller.stats.Energy.GetCalculatedStatValue() / controller.stats.Energy.BaseValue;
        staminaSlider.GetComponent<Slider>().value = (float)controller.stats.Stamina.GetCalculatedStatValue() / controller.stats.Stamina.BaseValue;

    }
}
