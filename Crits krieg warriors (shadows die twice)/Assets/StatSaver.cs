using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSaver : MonoBehaviour
{

    public string Name ;

    public int level = 0;

    public float maxHP = 30;
    public float cHP = 30;

    public int maxStamina =10;
    public int cStamina =10;

    public float atk =5;

    public int levelpoints = 0;

    public Swords firstSword;

    public Swords sword;



    public void setStats(string name ,int level, float maxHP, float cHP, int maxStamina, int cStamina, float atk, int levelpoints, Swords sword)
    {
        Name = name;
        this.level = level;
        this.maxHP = maxHP;
        this.cHP = cHP;
        this.maxStamina = maxStamina;
        this.cStamina = cStamina;
        this.atk = atk;
        this.levelpoints = levelpoints;
        this.sword = sword;

    }

    public void Reset()
    {
        this.level = 0;
        this.maxHP = 30;
        this.cHP = 30;
        this.maxStamina = 10;
        this.cStamina = 10;
        this.atk = 5;
        this.levelpoints = 0;
        this.sword = firstSword;
    }

    public void ApplyStats(Unit player)
    {
        player.level = level;
        player.maxHP = maxHP;
        player.cHP = cHP;
        player.maxStamina = maxStamina;
        player.cStamina = cStamina;
        player.atk = atk;
        player.levelpoints = levelpoints;
        player.sword = sword;
    }

}
