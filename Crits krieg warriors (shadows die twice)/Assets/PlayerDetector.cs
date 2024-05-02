using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    public float x;
    public float y;
    public Vector2 size;

    public bool playerspotted;

    public GateControl gateControl;


    // Start is called before the first frame update
    void Start()
    {
        size = new Vector2(x, y);
        playerspotted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerspotted == true)
        {
            return;
        }
        Collider2D playerfinder = Physics2D.OverlapBox(gameObject.transform.position,size,0);
        if (playerfinder!=null)
        {
            Player_Movement player_Movement = playerfinder.GetComponent<Player_Movement>();
            if (player_Movement != null)
            {
                playerspotted = true;
                CloseGate();
            }
        }
        
    }

    public void CloseGate()
    {
        gateControl.CloseGate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(gameObject.transform.position,new Vector3(x,y,0));
    }
}
