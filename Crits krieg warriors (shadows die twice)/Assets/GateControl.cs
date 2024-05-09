using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour
{

    Animator gatecontroller;
    public GameObject Leftgate;
    public GameObject BoxHolder1;
    public GameObject BoxHolder2;

    public PlayerDetector playerDetector;
    // Start is called before the first frame update
    void Start()
    {
        gatecontroller = gameObject.GetComponent<Animator>();
        playerDetector = gameObject.GetComponentInChildren<PlayerDetector>();
    }


    public void OpenGate()
    {
        BoxHolder1.SetActive(false);
        BoxHolder2.SetActive(false);
        gatecontroller.SetBool("Closing", false);
        gatecontroller.SetBool("Close", false);
        gatecontroller.SetBool("Open",true);
        gatecontroller.SetBool("Opening", true);

    }

    public void CloseGate()
    {
        BoxHolder1.SetActive(true);
        BoxHolder2.SetActive(true);
        gatecontroller.SetBool("Opening", false);
        gatecontroller.SetBool("Open", false);
        gatecontroller.SetBool("Closing", true);
        gatecontroller.SetBool("Close", true);

    }


    
    public void Update()
    {
        
    }

}
