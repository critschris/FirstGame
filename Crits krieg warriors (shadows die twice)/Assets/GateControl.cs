using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{

    Animator gatecontroller;
    public GameObject Leftgate;
    public GameObject BoxHolder1;
    public GameObject BoxHolder2;
    // Start is called before the first frame update
    void Start()
    {
        gatecontroller = gameObject.GetComponent<Animator>();
    }


    void OpenGate()
    {
        BoxHolder1.SetActive(false);
        BoxHolder2.SetActive(false);
        gatecontroller.SetBool("Open",true);
    }

    void CloseGate()
    {
        gatecontroller.SetBool("Close", true);
        gatecontroller.SetBool("Open", false);
        while (Leftgate.transform.localPosition.x!= -0.756)
        {
        }
        BoxHolder1.SetActive(true);
        BoxHolder2.SetActive(true);

        
        gatecontroller.SetBool("Close", false);

    }


}
