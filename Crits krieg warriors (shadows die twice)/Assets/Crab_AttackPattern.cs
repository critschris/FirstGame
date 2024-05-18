using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crab_AttackPattern : MonoBehaviour
{
    Unit CrabUnit;
    Rigidbody2D rb;
    Transform PlayerTransform;
    public bool Idle = true;
    public float detectionrange;
    public float dashspeed;
    public float dashtime;
    float dashtimecounter =0;
    bool Isdashcooldown = false;
    public float dashcooldowntime;
    float dashcooldowncounter = 0;
    public float walkforwardtime;
    float walkforwardcounter =0;


    public float walkspeed;
    bool Attacking =false;
    bool StartAttack = false;

    public float damage;

    Vector2 attackdirection;

    public Slider healthbar;

    [SerializeField]
    Animator animator;

    [SerializeField]
    TrailRenderer dashtrail;

    [SerializeField]
    Enemies_Effect_Manager Enemies_Effect_Manager;

    // Start is called before the first frame update
    void Start()
    {
        CrabUnit = GetComponent<Unit>();
        PlayerTransform = FindObjectOfType<Player_Movement>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
        dashtrail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Idle&&Vector3.Distance(gameObject.transform.position,PlayerTransform.position)< detectionrange)
        {
            Idle = false;
            Enemies_Effect_Manager.setAggro(true);
            StartCoroutine(DashAttack());

        }
        //No cooldown and not attacking then attack
        if (!Idle&&!Isdashcooldown&&!Attacking)
        {
            StartCoroutine(DashAttack());
        }

    }

    private void FixedUpdate()
    {
        if (StartAttack)
        {
            rb.MovePosition(rb.position + attackdirection * dashspeed * Time.fixedDeltaTime);
            dashtimecounter += Time.fixedDeltaTime;
            if (dashtimecounter > dashtime)
            {
                Isdashcooldown = true;
                Attacking = false;
                StartAttack = false;
                dashtrail.emitting = false;
                animator.SetBool("Dashing", false);
                animator.SetBool("Walking",true);
                dashtimecounter = 0;
            }
        }

        if (Isdashcooldown)
        {
            Vector2 walkingdirection = PlayerTransform.position - gameObject.transform.position;
            walkingdirection = walkingdirection.normalized;
            walkforwardcounter += Time.fixedDeltaTime;
            if (walkforwardcounter<walkforwardtime)
            {
                rb.MovePosition(rb.position + walkingdirection * walkspeed * Time.fixedDeltaTime);
            }
            else
            {
                walkingdirection = -walkingdirection;
                rb.MovePosition(rb.position + walkingdirection * walkspeed * Time.fixedDeltaTime);
            }
            
            
            dashcooldowncounter += Time.fixedDeltaTime;
            if (dashcooldowncounter>dashcooldowntime)
            {
                Isdashcooldown = false;
                animator.SetBool("Walking", false);
                walkforwardcounter = 0;
                dashcooldowncounter = 0;
            }
        }

        healthbar.value = CrabUnit.cHP/CrabUnit.maxHP;
        if (CrabUnit.checkDead())
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DashAttack()
    {
        Attacking = true;
        yield return new WaitForSeconds(0.5F);
        Vector3 attackdirection = PlayerTransform.position - gameObject.transform.position;
        this.attackdirection = attackdirection.normalized;
        StartAttack = true;
        animator.SetBool("Dashing",true);
        dashtrail.emitting = true;
        //FindObjectOfType<AudioManager>().Play("Crab Attack");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (StartAttack == true)
        {
            dashtimecounter = dashtime+1;
            Unit Unithit = collision.gameObject.GetComponent<Unit>();
            if (Unithit!=null)
            {
                Unithit.takeDamage(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, detectionrange);
    }
}
 