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

    public Animator RedOutLineAnimator;

    public bool attacking;
    public bool moving;
    public bool dead = false;

    public float speed;

    public Boss_Moona_WeaponParent boss_Moona_WeaponParent;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player_Movement>().gameObject.transform;
        BlackKnightUnit = GetComponent<Unit>();
        KnightAnimator = AnimatorHolder.GetComponent<Animator>();
        moving = true;
        KnightAnimator.SetBool("Walking", moving);

    }

    public IEnumerator Attack()
    {

        boss_Moona_WeaponParent.Playerposition = FindObjectOfType<Player_Movement>().gameObject.transform.position;
        boss_Moona_WeaponParent.Aim();
        RedOutLineAnimator.SetBool("Appear",true);
        yield return new WaitForSeconds(1F);
        KnightAnimator.SetTrigger("Attack");
        boss_Moona_WeaponParent.HitPlayer();
        boss_Moona_WeaponParent.AttackMethod(10F);
        RedOutLineAnimator.SetBool("Appear", false);

        yield return new WaitForSeconds(1F);
        attacking = false;

    }

    // Update is called once per frame
    void Update()
    {

        if (!attacking&&!dead)
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
            StartCoroutine(Death());
            
        }
    }

    public IEnumerator Death()
    {
        dead = true;
        KnightAnimator.SetBool("Death",dead);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void flip()
    {
        sprite.flipX = !sprite.flipX;
    }

    public void PlayerFinder()
    {
        Collider2D[] found_player = Physics2D.OverlapCircleAll(transform.position, 3, PlayerLayer);
        try
        {
            Collider2D Found = found_player[0];
            moving = false;
            KnightAnimator.SetBool("Walking",moving);
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
