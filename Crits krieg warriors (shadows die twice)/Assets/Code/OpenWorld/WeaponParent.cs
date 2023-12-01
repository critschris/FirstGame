using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }
    Vector2 direction;

    public Animator animator;
    public float delay = 0.3F;
    public bool attacking;
    public Transform attackarea;
    public Transform PlayerPosition;
    public LayerMask EnemyLayer;
    public float attackRange = 1.5F;

    public GameObject particleObject;
    public GameObject particleObject1;
    public GameObject particleObject2;

    public GameObject FlameThrowerPrefab;
    ConsistentDamageForFlameSkill FlameThrower;

    public void Awake()
    {
        FlameThrower = FlameThrowerPrefab.GetComponent<ConsistentDamageForFlameSkill>();
    }

    private void Update()
    {

        direction = (Pointerposition - (Vector2)transform.position).normalized;

        transform.right = direction;

        Vector2 scale = transform.localScale;
        Vector2 particlescale = particleObject.transform.localScale;
        if (direction.x < 0)
        {
            particlescale.y = -1;
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            particlescale.y = 1;
            scale.y = 1;
        }
        if (particleObject.activeSelf == true) {
            particleObject.transform.localScale = particlescale;
            particleObject1.transform.localScale = particlescale;
            particleObject2.transform.localScale = particlescale;
        }
        transform.localScale = scale;

    }

    public void AttackMethod(float playerdamage)
    {
        if (attacking) {
            return;
        }
        animator.SetTrigger("Attack");
        attacking = true;

        Collider2D[] enemies_hit = Physics2D.OverlapCircleAll(attackarea.position, attackRange, EnemyLayer);
        if (enemies_hit.Length > 0)
        {
            //
            //Attacked slash_animation = enemies_hit[0].GetComponentInChildren<Attacked>();
            // slash_animation.GetCut();

            //
            Unit enemy_hit = enemies_hit[0].GetComponent<Unit>();

            //take damage method has to come last because of damage number
            //Floating_Text damagenumber = enemy_hit.GetComponentInChildren<Floating_Text>();
            enemy_hit.takeDamage(playerdamage);

        }

        StartCoroutine(DelayAttack());
    }

    public void Ability2Active(float playerdamage, Swords sword)
    {
        Debug.Log("Ability 2 Activated choosing ability");
        if (sword.getName() == "Toy Light Saber")
        {
            Debug.Log("Ability 2 Activated But no ability");
            return;
        }
        else if (sword.getName()== "Big Burtha") {
            Debug.Log("Ability 2 for big burtha found");
            float seventypercent = playerdamage * 0.1F;
            StartCoroutine(BigBurthaAbility2(seventypercent));

        }

    }

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attacking = false;
    }

    //Collection of ability2s
    IEnumerator BigBurthaAbility2(float playerdamage)
    {
        Debug.Log("Ability 2 Activated");
        FlameThrower.damage = playerdamage;


        for (int i = 0; i < 10; i++)
        {
            FlameThrower.direction = direction;
            Instantiate(FlameThrowerPrefab, PlayerPosition.position + new Vector3(direction.x,direction.y*2,0), Quaternion.identity);
            yield return new WaitForSeconds(0.2F);

        }
        FlameThrower.direction = direction;
        Instantiate(FlameThrowerPrefab, PlayerPosition.position + new Vector3(direction.x*2, direction.y*2, 0), Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackarea.position,attackRange);
    }
}
