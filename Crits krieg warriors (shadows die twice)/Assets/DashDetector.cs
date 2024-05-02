using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDetector : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2D;
    TutorialGameManager gameMan;
    Dialogue dialogue;
    // Update is called once per frame
    public void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        gameMan = FindObjectOfType<TutorialGameManager>();
        dialogue = FindObjectOfType<Dialogue>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (dialogue.index > 11) {
            Player_Movement Player = collision.gameObject.GetComponent<Player_Movement>();
            if (Player != null)
            {
                if (Player.dashing == true && !gameMan.oncefordashthroughbox)
                {
                    gameMan.ActivateDialogue();
                    gameMan.oncefordashthroughbox = true;
                }
            }
        }
    }

}
