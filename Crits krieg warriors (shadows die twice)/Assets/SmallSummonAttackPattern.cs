using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallSummonAttackPattern : MonoBehaviour
{
    public float speed;
    bool Rooted;
    public LayerMask PlayerLayer;
    public float AttackRange;
    public Transform target;
    public GameObject SpriteHolder;
    SpriteRenderer Sprite_Fire_Spirit;
    public GameObject AnimatorHolder;
    Animator REDanimator;

    public Slider SummonHealth;
    Unit SummonUnit;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<Player_Movement>().gameObject.transform;
        Sprite_Fire_Spirit = SpriteHolder.GetComponent<SpriteRenderer>();
        REDanimator = AnimatorHolder.GetComponent<Animator>();
        SummonUnit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!Rooted)
        {

            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);

            PlayerFinder();
        }

        if (target.position.x >= transform.position.x)
        {
            if (Sprite_Fire_Spirit.flipX == true)
            {
                flip();
            }
            
        }
        else
        {
            if (Sprite_Fire_Spirit.flipX == false)
            {
                flip();
            }
            
        }

        SummonHealth.value = (SummonUnit.cHP / SummonUnit.maxHP);

        if (SummonUnit.cHP < 0)
        {
            Destroy(gameObject);
        }
    }


    public void PlayerFinder()
    {
        Collider2D[] found_player = Physics2D.OverlapCircleAll(transform.position, AttackRange, PlayerLayer);
        try
        {
            Collider2D Found = found_player[0];
            Rooted = true;
            StartCoroutine(PlantAndDamageEverycyle());
        }
        catch (System.Exception nothingfound)
        {
            Rooted = false;
            return;
        }
    }

    void flip()
    {
        Sprite_Fire_Spirit.flipX = !Sprite_Fire_Spirit.flipX;
    }

    public IEnumerator PlantAndDamageEverycyle()
    {

        for (int i =0; i<10;i++)
        {
            Attack();
            yield return new WaitForSeconds(0.5F);
        }
        //Attack();
        yield return new WaitForSeconds(3F);
        Rooted = false;
    }
    public void Attack()
    {
        REDanimator.SetTrigger("Damage");
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.5F, 1.5F), 0, PlayerLayer);
        if (hit.Length > 0)
        {
            hit[0].GetComponent<Unit>().takeDamage(1F);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(1.5F, 1.5F));
    }
}
