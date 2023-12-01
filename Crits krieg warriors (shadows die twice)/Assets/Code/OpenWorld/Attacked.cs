using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacked : MonoBehaviour
{

    public Animator animator;

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void GetCut()
    {
        animator.SetTrigger("Attacked");
    }

   
}
