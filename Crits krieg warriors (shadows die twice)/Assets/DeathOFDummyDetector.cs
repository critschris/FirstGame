using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOFDummyDetector : MonoBehaviour
{
    public DummyHealthUpdate[] dummies;
    bool alldummiesdead = false;
    TutorialGameManager tutorialGM;
    bool once;

    private void Start()
    {
        tutorialGM = FindObjectOfType<TutorialGameManager>();

    }
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

        if (!once&&alldummiesdead)
        {
            tutorialGM.ActivateDialogue();
            tutorialGM.incrementcurrentgate();
            once = true;
        }
    }
}
