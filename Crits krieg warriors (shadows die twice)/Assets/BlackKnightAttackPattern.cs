using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackKnightAttackPattern : MonoBehaviour
{
    public LayerMask PlayerLayer;

    public SpriteRenderer sprite;
    public Transform target;
    public Slider BlackKnightHealth;
    Unit BlackKnightUnit;

    public GameObject AnimatorHolder;
    Animator KnightAnimator;

    public bool attacking;
    public bool moving;

    public float speed;

    public Boss_Moona_WeaponParent boss_Moona_WeaponParent;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player_Movement>().gameObject.transform;
        BlackKnightUnit = GetComponent<Unit>();
        KnightAnimator = AnimatorHolder.GetComponent<Animator>();

    }

    public IEnumerator Attack()
    {
        KnightAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.15F);
        boss_Moona_WeaponParent.Playerposition = FindObjectOfType<Player_Movement>().gameObject.transform.position;
        boss_Moona_WeaponParent.Aim();
        //Red Area
        yield return new WaitForSeconds(1F);
        boss_Moona_WeaponParent.HitPlayer();
        boss_Moona_WeaponParent.AttackMethod(10F);

        yield return new WaitForSeconds(3F);
        attacking = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!attacking)
        {
            moving = true;
            KnightAnimator.SetBool("Walking",moving);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

            PlayerFinder();
        }

        if (target.position.x >= transform.position.x)
        {
            if (sprite.flipX == true)
            {
                flip();
            }

        }
        else
        {
            if (sprite.flipX == false)
            {
                flip();
            }

        }

        BlackKnightHealth.value = (BlackKnightUnit.cHP / BlackKnightUnit.maxHP);

        if (BlackKnightUnit.cHP < 0)
        {
            Destroy(gameObject);
        }
    }

    void flip()
    {
        sprite.flipX = !sprite.flipX;
    }

    public void PlayerFinder()
    {
        Collider2D[] found_player = Physics2D.OverlapCircleAll(transform.position, 1, PlayerLayer);
        try
        {
            Collider2D Found = found_player[0];
            moving = false;
            KnightAnimator.SetBool("Walking", moving);
            attacking = true;
            StartCoroutine(Attack());
        }
        catch (System.Exception nothingfound)
        {
            attacking = false;
            return;
        }
    }

}
