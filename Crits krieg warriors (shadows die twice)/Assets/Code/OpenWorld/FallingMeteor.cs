using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMeteor : MonoBehaviour
{

    public Animator BallAnimator;
    public Animator ImpactAnimator;
    public GameObject target;
    public GameObject Meteor;
    public float attackradius;

    private void Start()
    {
        StartCoroutine(Explode());
        Destroy(gameObject,2F);

    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(1F);
        Meteor.SetActive(false);
        ImpactAnimator.SetTrigger("Impact");
        FindObjectOfType<AudioManager>().Play("Meteor");
        Collider2D[] hit_enemies = Physics2D.OverlapCircleAll(transform.position,attackradius);
        if (hit_enemies.Length>0)
        {
            for (int i =0; i<hit_enemies.Length;i++)
            {
                if (hit_enemies[i].GetComponent<Unit>()!=null)
                {
                    hit_enemies[i].GetComponent<Unit>().takeDamage(20F);
                }
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,attackradius);
    }
}
