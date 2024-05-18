using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaHorse_AttackPattern : MonoBehaviour
{
    Unit SeahorseUnit;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    Transform PlayerTransform;
    public float walkspeed;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Enemies_Effect_Manager Enemies_Effect_Manager;

    bool Idle = true;
    public float detectionrange;
    public float AttackCoolDowntime;
    float AttackCoolDowncounter=0;
    bool IsAttackCooldown =false;
    bool Attacking = false;

    [SerializeField]
    MoonaFireWeaponParent rangeweaponparent;

    [SerializeField]
    Slider healthbar;

    [SerializeField]
    SpriteRenderer seahorsesprite;
    // Start is called before the first frame update
    void Start()
    {
        SeahorseUnit = GetComponent<Unit>();
        PlayerTransform = FindObjectOfType<Player_Movement>().gameObject.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Idle && Vector3.Distance(gameObject.transform.position, PlayerTransform.position) < detectionrange)
        {
            Idle = false;
            Enemies_Effect_Manager.setAggro(true);
            StartCoroutine(RangeAttack());
        }
        if (!Idle&&!IsAttackCooldown&&!Attacking)
        {
            StartCoroutine(RangeAttack());
        }

        if (PlayerTransform.position.x >= transform.position.x && seahorsesprite.flipX)
        {
            seahorsesprite.flipX = false;
        }
        else if(PlayerTransform.position.x < transform.position.x && !seahorsesprite.flipX)
        {
            seahorsesprite.flipX = true;
        }

    }

    void FixedUpdate()
    {

        if (IsAttackCooldown)
        {
            Vector2 walkingdirection = PlayerTransform.position - gameObject.transform.position;
            walkingdirection = walkingdirection.normalized;
            if (Vector3.Distance(gameObject.transform.position, PlayerTransform.position) < detectionrange)
            {
                walkingdirection = -walkingdirection;
                rb.MovePosition(rb.position + walkingdirection * walkspeed * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + walkingdirection * walkspeed * Time.fixedDeltaTime);
            }

            AttackCoolDowncounter += Time.fixedDeltaTime;
            if (AttackCoolDowncounter>AttackCoolDowntime)
            {
                IsAttackCooldown = false;
                AttackCoolDowncounter = 0;
                animator.SetBool("Walking", false);
            }
        }
        healthbar.value = SeahorseUnit.cHP / SeahorseUnit.maxHP;
        if (SeahorseUnit.checkDead())
        {
            Destroy(gameObject);
        }
    }

    IEnumerator RangeAttack() {
        Attacking = true;
        if (!seahorsesprite.flipX)
        {
            animator.SetTrigger("Right Attack");
        }
        else
        {
            animator.SetTrigger("Left Attack");
        }
        yield return new WaitForSeconds(0.25F);
        rangeweaponparent.Aim();
        //SFX for attack
        rangeweaponparent.Fire();
        IsAttackCooldown = true;
        Attacking = false;
        animator.SetBool("Walking",true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, detectionrange);
    }
}
