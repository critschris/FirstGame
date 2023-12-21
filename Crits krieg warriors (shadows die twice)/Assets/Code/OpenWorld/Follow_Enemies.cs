using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_Enemies : MonoBehaviour
{
    public float speed;
    public Transform target;
    public LayerMask PlayerLayer;
    public GameObject SpriteHolder;
    SpriteRenderer Sprite_Fire_Spirit;
    Animator animator;
    public GameObject SpriteExplosionHolder;
    Animator explosion_animator;
    public float AttackRange;
    public float explode_range;
    bool charging;
    bool tired;
    public float charging_time;
    public float tired_time;
    public float exp_give;
    //public GameObject audiomanager_holder;
    //AudioManager audiomanager;

    void Start()
    {
        Sprite_Fire_Spirit = SpriteHolder.GetComponent<SpriteRenderer>();
        animator = SpriteHolder.GetComponent<Animator>();
        explosion_animator = SpriteExplosionHolder.GetComponent<Animator>();
       // audiomanager = audiomanager_holder.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //found_player = new Collider2D[1];
        
       if (!charging&&!tired) {
            
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            
            PlayerFinder();
        }


        if (target.position.x>=transform.position.x)
        {
            if (Sprite_Fire_Spirit.flipX==true) {
                flip();
            }
            else
            {
                return;
            }
        }
        else
        {
            if (Sprite_Fire_Spirit.flipX == false)
            {
                flip();
            }
            else
            {
                return;
            }
        }

    }

    void flip()
    {
        Sprite_Fire_Spirit.flipX = !Sprite_Fire_Spirit.flipX;
    }

    IEnumerator ChargeAttack()
    {
        animator.SetBool("Charging", charging);
        yield return new WaitForSeconds(charging_time);
        Collider2D[] try_to_hit = Physics2D.OverlapCircleAll(transform.position, explode_range, PlayerLayer);
        FindObjectOfType<AudioManager>().Play("explosion");
        try
        {

            animator.SetTrigger("Attack");
            explosion_animator.SetTrigger("Attack");
            
            Collider2D Hit_Player = try_to_hit[0];
            
            
            
            Unit hit = Hit_Player.GetComponent<Unit>();
            hit.takeDamage(10F);

            charging = false;
            animator.SetBool("Charging", false);
  

        }
        catch (System.Exception noplayer)
        {
            animator.SetBool("Charging", false);
            charging = false;
        }
        StartCoroutine(Tired());

        
    }

    IEnumerator Tired()
    {
        tired = true;
        yield return new WaitForSeconds(tired_time);
        tired = false;
    }

    public void PlayerFinder()
    {
        Collider2D[] found_player = Physics2D.OverlapCircleAll(transform.position, AttackRange, PlayerLayer);
        try
        {
            Collider2D Found = found_player[0];
            charging = true;
            StartCoroutine(ChargeAttack());
        }catch(System.Exception nothingfound)
        {
            charging = false;
            return;
        }
    }

   

    void OnDrawGizmosSelected()
    {
        if (transform.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(transform.position, AttackRange);
        Gizmos.DrawWireSphere(transform.position, explode_range);

    }
}
