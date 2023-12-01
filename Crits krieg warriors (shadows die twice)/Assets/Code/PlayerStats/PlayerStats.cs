using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat", menuName = "Sword/Create new Player Stat")]
public class PlayerStats : ScriptableObject
{

    [SerializeField] string Name;

    [SerializeField] int level;

    [SerializeField] float maxHP;
    [SerializeField] float cHP;

    [SerializeField] int maxStamina;
    [SerializeField] int cStamina;

    [SerializeField] float atk;

    [SerializeField] int levelpoints;

    [SerializeField] Swords sword;
    /*
     *   public string name;
    public int level;

    public float maxHP;
    public float cHP;

    public int maxMP;
    public int cMP;

    public int maxStamina;
    public int cStamina;

    public float atk;
     */

    public void setName(string a)
    {
        Name = a;
    }

    public string getName()
    {
        return Name;
    }

    public void setlevel(int a)
    {
        level = a;
    }

    public int getlevel()
    {
        return level;
    }

    public float getmaxHP()
    {
        return maxHP;
    }

    public float getcHP()
    {
        return cHP;
    }

    public void setHP(float maxHP, float cHP)
    {
        this.maxHP = maxHP;
        this.cHP = cHP;
    }

    public void setStamina(int a)
    {
        maxStamina = a;
        cStamina = a;
    }

    public int getmaxStamina()
    {
        return maxStamina;
    }

    public int getcStamina()
    {
        return cStamina;
    }

    public void setatk(float a)
    {
        atk = a;
    }

    public float getatk()
    {
        return atk;
    }

    public void setsword(Swords a)
    {
        sword = a;
    }

    public Swords getsword()
    {
        return sword;
    }

    public void setlevelpoints(int a)
    {
        levelpoints = a;
    }

    public int getlevelpoints()
    {
        return levelpoints;
    }
}
