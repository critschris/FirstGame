using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Effect_Manager : MonoBehaviour
{

    bool Aggro;

    bool[] AllEffects;

    [SerializeField]
    Animator animator;

    void Start()
    {
        //Fill Array with all Effects/ Prob gonna have to be manual

    }

    public void setAggro(bool a)
    {
        Aggro = a;
    }

    // Update is called once per frame
    void Update()
    {
        if (Aggro)
        {
            //Set all other effects to false
            StartCoroutine(Aggroed());
            Aggro = false;
        }
    }

    IEnumerator Aggroed()
    {
        animator.SetBool("Aggro",true);
        yield return new WaitForSeconds(1F);
        animator.SetBool("Aggro", false);
    }
}
