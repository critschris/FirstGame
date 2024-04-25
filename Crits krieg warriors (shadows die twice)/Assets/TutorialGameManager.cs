using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameManager : MonoBehaviour
{

    Player_Movement PlayerMovementComponent;
    bool dashtutorialstart = false;
    Dialogue dialogue;

    public GateControl[] gates;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementComponent = FindObjectOfType<Player_Movement>();
        dialogue = FindObjectOfType<Dialogue>();

        StartCoroutine(TutorialStart());
    }

    IEnumerator TutorialStart()
    {
        yield return new WaitForSeconds(3F);
        dialogue.StartDialogue();
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dashtutorialstart) { 
            PlayerMovementComponent.IsdashCoolDown = true;
        }
    }
}
