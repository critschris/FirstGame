using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOFDummyDetector : MonoBehaviour
{
    public DummyHealthUpdate[] dummies;
    bool alldummiesdead = false;

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i< dummies.Length;i++)
        {
            if (dummies[i]!=null) {
                return;
            }
        }
        alldummiesdead = true;
    }
}
