using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundChanger : MonoBehaviour
{
    public GameObject [] bgs;

    BackGround[] backGrounds;

    private void Start()
    {
        backGrounds = new BackGround[bgs.Length];
        for(int i = 0; i<bgs.Length;i++)
        {
            backGrounds[i] = bgs[i].GetComponent<BackGround>();
        }
    }
    public void bgChange(int a)
    {


        for (int i = 0; i < bgs.Length; i++)
        {
            backGrounds[i].toggle(false);
        }

        backGrounds[a].toggle(true);


    }
}
