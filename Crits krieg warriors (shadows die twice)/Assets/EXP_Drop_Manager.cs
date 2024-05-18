using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXP_Drop_Manager : MonoBehaviour
{
    Transform PlayerTransform;
    int AmountDrop;

    EXPBall [] expballs;

    [SerializeField]
    GameObject EXP_ball_prefab;

    [SerializeField]
    float pickuprange;
    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = FindObjectOfType<Player_Movement>().gameObject.transform;
        DropEXP();
    }

    void Update()
    {
        if (Vector3.Distance(PlayerTransform.position,gameObject.transform.position)<pickuprange)
        {
            foreach(EXPBall c in expballs)
            {
                c.ActivateBall();
            }

            //Add exp to playerexpmanager
            Destroy(gameObject);
        }
    }

    public void setAmountDrop(int a)
    {
        AmountDrop = a;
    }

    public void setpickuprange(float a)
    {
        pickuprange = a;
    }

    public float getpickuprange() {
        return pickuprange;
    }

    public void DropEXP()
    {
        int tempnum = Mathf.FloorToInt(AmountDrop);
        expballs = new EXPBall[tempnum];
        for (int i =0;i<tempnum ;i++)
        {
            expballs[0]=Instantiate(EXP_ball_prefab,gameObject.transform.position,Quaternion.identity).GetComponent<EXPBall>();
        }
    }
}
