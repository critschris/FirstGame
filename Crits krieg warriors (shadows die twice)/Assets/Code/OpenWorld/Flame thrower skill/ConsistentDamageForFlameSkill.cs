using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsistentDamageForFlameSkill : MonoBehaviour
{
   
    public float destroytime;
    private float time = 0;
    public float damage;
    public float speed;
    public int count = 2;
    int counter = 0;
    public LayerMask EnemyLayer;
    public Vector2 direction;
    public Vector2 hitboxsize;

    void Start()
    {
        Destroy(gameObject,destroytime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time > 0.1)
        {
            Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, hitboxsize, 0, EnemyLayer);
            if ((hit.Length > 0)&&(count!=counter))
            {
                for (int i =0;i<hit.Length ;i++) {
                    hit[i].GetComponent<Unit>().takeDamage(damage/hit.Length);
                }
                counter++;
            }
            time = 0;
        }
        time += Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position,transform.position+new Vector3(direction.x,direction.y,0),speed*Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, hitboxsize);
    }
}
