using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Text : MonoBehaviour
{

    public float destroy_time = 1f;

    // Start is called before the first frame update
    void Start()
    {

        Destroy(gameObject, destroy_time);
    }


   
}
