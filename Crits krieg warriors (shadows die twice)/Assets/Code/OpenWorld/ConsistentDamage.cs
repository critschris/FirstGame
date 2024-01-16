using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentDamage : MonoBehaviour
{
    private float time =0;
    public LayerMask PlayerLayer;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time>1)
        {
            Collider2D [] hit = Physics2D.OverlapBoxAll(transform.position,new Vector2(1,1),0,PlayerLayer);
            if (hit.Length>0)
            {
                hit[0].GetComponent<Unit>().takeDamage(1F);
            }
            time = 0;
        }
        time += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector2(1,1));
    }
}
