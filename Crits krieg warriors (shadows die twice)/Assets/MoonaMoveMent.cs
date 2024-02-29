using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonaMoveMent : MonoBehaviour
{

    public Transform destination;
    public bool moving = false;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            //Debug.Log("Moving");
            transform.position = Vector3.MoveTowards(gameObject.transform.position, destination.position, Time.fixedDeltaTime * speed);
            CloseEnough();
        }
    }

    public void CloseEnough()
    {
        float Bossxcoordinate = gameObject.transform.position.x;
        float Bossycoordiante = gameObject.transform.position.y;
        if (destination.position.x - 1 < Bossxcoordinate && Bossxcoordinate < destination.position.x + 1 && destination.position.y - 1 < Bossycoordiante && Bossycoordiante < destination.position.y + 1)
        {
            moving = false;
        }

    }
}
