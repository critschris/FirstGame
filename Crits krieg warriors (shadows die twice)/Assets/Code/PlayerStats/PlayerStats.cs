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

    public void setStats(string name, int level, float maxHP, float cHP, int maxStamina, int cStamina, float atk, int levelpoints, Swords sword)
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
