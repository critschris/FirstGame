using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPBall : MonoBehaviour
{

    bool Idle = true;

    [SerializeField]
    float flyingspeed;

    Transform PlayerTransform;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = FindObjectOfType<Player_Movement>().gameObject.transform;
    }

    public void ActivateBall()
    {
        Idle = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Idle)
        {
            float step = flyingspeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, step);
            if (Vector3.Distance(transform.position, PlayerTransform.position)<0.5)
            {
                Destroy(gameObject);
            }
        }
    }
}
