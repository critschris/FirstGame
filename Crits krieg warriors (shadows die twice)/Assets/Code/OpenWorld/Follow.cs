using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    public float followspeed;
    public Transform follow;

    // Update is called once per frame
    void Update()
    {

        Vector3 newPOS = new Vector3(follow.position.x, follow.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position,newPOS,followspeed*Time.deltaTime);

    }
}
