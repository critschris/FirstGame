using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar_Interaction : MonoBehaviour
{
    public LayerMask Player;
    public Animator animator;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position,2f,Player);
        if (player!=null)
        {
            animator.SetBool("Appear",true);
        }
        else
        {
            animator.SetBool("Appear",false); 
        }

        return;

    }

    
}
