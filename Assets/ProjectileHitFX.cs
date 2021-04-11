using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHitFX : MonoBehaviour
{
    public HashSet<int> AttackedEnemies = new HashSet<int>();
    public PlayerSkills.DamageSpell currSkill = new PlayerSkills.Lightning();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Enemy" && !AttackedEnemies.Contains(other.GetInstanceID()))
        {
            AttackedEnemies.Add(other.GetInstanceID());
            if (currSkill.type == (int)PlayerSkills.SkillType.Basic)
            {
                other.GetComponent<FootmanScript>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 10);
            }
            //else if (currSkill.type == (int)PlayerSkills.SkillType.Dot)
            //{
            //    PlayerSkills.PoisonousFumes p = (PlayerSkills.PoisonousFumes)currSkill;
            //    StartCoroutine(Dot(other, p.duration, p.interval));
            //}
        }
        else if (other.tag == "Boss" && !AttackedEnemies.Contains(other.GetInstanceID()))
        {
            AttackedEnemies.Add(other.GetInstanceID());
            if (currSkill.type == (int)PlayerSkills.SkillType.Basic)
            {
                other.GetComponent<Level1Boss>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 10);
            }
            //else if (currSkill.type == (int)PlayerSkills.SkillType.Dot)
            //{
            //    PlayerSkills.PoisonousFumes p = (PlayerSkills.PoisonousFumes)currSkill;
            //    StartCoroutine(Dot(other, p.duration, p.interval));
            //}
        }
    }
}
