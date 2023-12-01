using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword_In_Slot : MonoBehaviour
{
    public Swords sword_In_Slot;
    public Image image;
    bool firsttime = true;

    public void Update()
    {
        image = gameObject.GetComponentInChildren<Image>();

        if (sword_In_Slot!= null) {
            image.sprite = sword_In_Slot.sword;
            //firsttime = false;
        }
    }


}
