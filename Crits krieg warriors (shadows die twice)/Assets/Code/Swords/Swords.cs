using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sword",menuName = "Sword/Create new sword")]
public class Swords : ScriptableObject
{

    [SerializeField] string Name;

    [TextArea]
    [SerializeField] string Description;

    [SerializeField] public Sprite sword;


    [SerializeField] Rarity rarity;

    [SerializeField] float Scaling;

    [SerializeField] float Ability2CoolDown;

    public string getName()
    {
        return Name;
    }

    public string getDescription()
    {
        return Description;
    }

    public float getScaling()
    {
        return Scaling;
    }

    public Sprite getSprite()
    {
        return sword;
    }

    public float getAbility2CoolDown()
    {
        return Ability2CoolDown;
    }
}

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
