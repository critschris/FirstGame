using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOfGokuDetector : MonoBehaviour
{
    public GameObject Goku;
    
    public TutorialGameManager tutorialGM;
    // Start is called before the first frame update
    void Start()
    {
        tutorialGM = FindObjectOfType<TutorialGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Goku==null)
        {
            tutorialGM.ActivateDialogue();
            tutorialGM.incrementcurrentgate();
            FindObjectOfType<AudioManager>().StopAll();
            FindObjectOfType<AudioManager>().Play("Doki Doki");
            Destroy(gameObject);
        }
    }
}
