using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHUD : MonoBehaviour
{

    public Text playername;
    public Text HP;
    public Slider HPslider;
    public Text MP;
    public Slider MPslider;
    



    public void SetupHUD(Unit unit)
    {

        playername.text = unit.name;

        HP.text = unit.maxHP+"";
        HPslider.maxValue = unit.maxHP;
        HPslider.value = unit.cHP;

        MP.text = unit.maxMP+"";
        MPslider.maxValue = unit.maxMP;
        MPslider.value = unit.cMP;

    }

    public void HPupdate(Unit unit)
    {
        HP.text = unit.cHP+"";
        HPslider.value = unit.cHP;

        MP.text = unit.cMP+"";
        MPslider.value = unit.cMP;
    }
}
