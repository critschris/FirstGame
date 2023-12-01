using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

    public GameObject floating_Textprefab;
    public Transform position_of_damage;

    public string name;
    public int level;

    public float maxHP;
    public float cHP;

    public int maxMP;
    public int cMP;

    public int maxStamina;
    public int cStamina;

    public float atk;

    public bool Isdead = false;
    public bool Stunned = false;
    public bool Shielded = false;
    public bool LevelUp = false;
    public bool Invulnarable = false;

    public int levelpoints = 0;


    public Swords sword;

    public void EquipSword(Swords a)
    {
        sword = a;
    }


    void ShowDamage(float damage)
    {
        Text temp = floating_Textprefab.GetComponentInChildren<Text>();
        temp.text = damage + "";
        Instantiate(floating_Textprefab,position_of_damage.position,Quaternion.identity,transform);
        


    }

    public bool takeDamage(float damage)
    {

        float tempdamage = damage;

        if (Shielded==true)
        {
            tempdamage = damage * 0.5F;
        }
        if (Invulnarable ==true)
        {
            tempdamage = 0;
        }

            float currenthp = this.cHP - tempdamage;

            if (floating_Textprefab != null)
            {
                ShowDamage(tempdamage);
            }

            cHP = currenthp;
            
        

        return checkDead();
    }

    public void setStunned(bool a)
    {
        Stunned = a;
    }

    public void AddMaxHealth()
    {
        float temp = maxHP * 0.5F;
        maxHP += temp;
        cHP+=temp;
        
    }

    public bool checkPlayerUpgradePoints()
    {
        if (levelpoints > 0)
        {
            return true;
        }
        return false;
    }

    public void reducePlayerUpgradePoints()
    {
        levelpoints -=1;
    }

    public void Addatk()
    {
        atk += atk*0.5F;
    }



    public bool checkDead()
    {
        if (cHP<=0)
        {
            Isdead = true;
            return Isdead;
        }
        else
        {
            Isdead = false;
            return Isdead;
        }
    }
}
