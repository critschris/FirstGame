using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterSpotter : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        StartCoroutine(TeleporationAnimation());
    }

    IEnumerator TeleporationAnimation()
    {
        animator.SetBool("Activate",true);
        yield return new WaitForSeconds(1F);
        
        FindObjectOfType<TutorialGameManager>().ActivateFinishedTutorial();
    }
}
