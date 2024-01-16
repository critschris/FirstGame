using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerAttack : MonoBehaviour
{
    public float timer = 2F;
    float counter = 0;
    public MoonaFireWeaponParent moonaFireWeaponParent;
    public Slider health;
    public Unit towerunit;

    private void FixedUpdate()
    {
        if (counter>timer)
        {
            moonaFireWeaponParent.Aim();
            moonaFireWeaponParent.Fire();
            counter = 0;
        }
        else
        {
            counter += Time.fixedDeltaTime;
        }

        health.value = (towerunit.cHP / towerunit.maxHP);

        if (towerunit.cHP<0)
        {
            Destroy(gameObject);
        }
    }
}
