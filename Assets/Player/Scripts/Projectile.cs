using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect = true;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    //private ParticleSystem ps;
    public HashSet<int> AttackedEnemies = new HashSet<int>();
    public int Damage = 10;
    public PlayerSkills.DamageSpell currSkill = new PlayerSkills.Fireball();
    public bool flag = false;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.tag);
        if (other.tag == "Enemy" && !AttackedEnemies.Contains(other.GetInstanceID()))
        {
            AttackedEnemies.Add(other.GetInstanceID());
            if (currSkill.type == (int)PlayerSkills.SkillType.Basic)
            {
                other.GetComponent<FootmanScript>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 2);
                Col(other);
            }
            else if (currSkill.type == (int)PlayerSkills.SkillType.Dot)
            {
                PlayerSkills.PoisonousFumes p = (PlayerSkills.PoisonousFumes)currSkill;
                StartCoroutine(Dot(other, p.duration, p.interval));
            }
        }
        else if (other.tag == "Boss" && !AttackedEnemies.Contains(other.GetInstanceID()))
        {
            AttackedEnemies.Add(other.GetInstanceID());
            if (currSkill.type == (int)PlayerSkills.SkillType.Basic)
            {
                if (other.GetComponent<Level1Boss>())
                    other.GetComponent<Level1Boss>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 3);
                if (other.GetComponent<Level2Boss>())
                    other.GetComponent<Level2Boss>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 3);
                if (other.GetComponent<Level3Boss>())
                    other.GetComponent<Level3Boss>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 3);
                Col(other);
            }
            else if (currSkill.type == (int)PlayerSkills.SkillType.Dot)
            {
                PlayerSkills.PoisonousFumes p = (PlayerSkills.PoisonousFumes)currSkill;
                StartCoroutine(Dot(other, p.duration, p.interval));
            }
        }
        else
        {
            Col(other);
        }
    }

    public IEnumerator Dot(GameObject other, float duration, float interv)
    {
        int t = (int)(duration / interv);
        while(t-- > 0)
        {
            if (other.tag == "Enemy")
                other.GetComponent<FootmanScript>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 1);
            if (other.GetComponent<Level1Boss>())
                other.GetComponent<Level1Boss>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 1);
            if (other.GetComponent<Level2Boss>())
                other.GetComponent<Level2Boss>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 1);
            if (other.GetComponent<Level3Boss>())
                other.GetComponent<Level3Boss>().setDamage((int)currSkill.mod[currSkill.skillLevel] / 1);

            yield return new WaitForSeconds(interv);
        }
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) { instance.transform.rotation = Quaternion.Euler(rotationOffset); }
                else
                {
                    instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                }
                Destroy(instance, DestroyTimeDelay);
            }
        }
        if (DestoyMainEffect == true)
        {
            Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
    }
    public void Col(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                if (!UseWorldSpacePosition) instance.transform.parent = transform;
                if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) { instance.transform.rotation = Quaternion.Euler(rotationOffset); }
                else
                {
                    instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                    instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                }
                Destroy(instance, DestroyTimeDelay);
            }
        }
        if (DestoyMainEffect == true)
        {
            Destroy(gameObject, DestroyTimeDelay + 0.5f);
        }
    }
}