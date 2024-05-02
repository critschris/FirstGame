using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameManager : MonoBehaviour
{

    Player_Movement PlayerMovementComponent;
    Unit PlayerUnit;
    public ListOfSwords swordlist;
    bool dashtutorialstart = false;
    bool attacktutorial = false;
    Dialogue dialogue;
    public GameObject DialogueBox;

    DeathOFDummyDetector DeathOFDummyDetector;

    public bool oncegate1;
    public bool oncegate2;

    public bool oncefordashtut;

    public bool oncefordashinput;

    public bool oncefordashthroughbox;

    public bool onceforattacktutorial;

    public GameObject UI_ofPlayer;

    public GateControl[] gates;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementComponent = FindObjectOfType<Player_Movement>();
        dialogue = FindObjectOfType<Dialogue>();
        DeathOFDummyDetector = FindObjectOfType<DeathOFDummyDetector>();
        oncegate1 = false;
        oncegate2 = false;
        oncefordashtut = false;
        oncefordashinput = false;
        oncefordashthroughbox = false;
        onceforattacktutorial = false;
        PlayerUnit = PlayerMovementComponent.gameObject.GetComponent<Unit>();
        PlayerUnit.EquipSword(swordlist.translateindexintosword("Toy Light Saber"));
        StartCoroutine(TutorialStart());
    }

    IEnumerator TutorialStart()
    {
        yield return new WaitForSeconds(0.1F);
        dialogue.StartDialogue();
    }

    void Update()
    {
        if (dialogue.currentgate<gates.Length) {
            if (dialogue.index == dialogue.indexforopengate[dialogue.currentgate])
            {
                //Make new gate everytime you have new module cause I bad at code
                if (!oncegate1&&dialogue.currentgate==0)
                {
                    gates[0].OpenGate();
                    oncegate1 = true;
                }else if (!oncegate2&&dialogue.currentgate==1)
                {
                    gates[1].OpenGate();
                    oncegate2 = true;
                }
            }
            if (gates[dialogue.currentgate].playerDetector.playerspotted == true)
            {
                if (dialogue.currentgate==0) {
                    dashtutorialstart = true;
                }else if (dialogue.currentgate == 1)
                {
                    attacktutorial = true;
                }
            }
        }
        if (!oncefordashtut&&dashtutorialstart)
        {
            
            UI_ofPlayer.SetActive(true);
            ActivateDialogue();
            oncefordashtut = true;
        }
        if (!oncefordashinput&&UI_ofPlayer.activeInHierarchy && PlayerMovementComponent.dashing&& !DialogueBox.activeInHierarchy)
        {
            ActivateDialogue();
            oncefordashinput = true;
            dialogue.currentgate++;
        }
        if (!onceforattacktutorial&&attacktutorial)
        {
            ActivateDialogue();
            onceforattacktutorial = true;
        }
        
        //Increment the currentgate when you detect that all dummies have been killed

    }

    public void ActivateDialogue()
    {
        DialogueBox.SetActive(true);
        dialogue.NextLine();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dashtutorialstart) { 
            PlayerMovementComponent.IsdashCoolDown = true;
        }
    }
}
