using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Moona_WeaponParent : MonoBehaviour
{
    public Vector2 Playerposition { get; set; }

    public Transform attackarea;
    public LayerMask PlayerLayer;
    public float attackRangeX = 27F;
    public float attackRangeY = 4F;
    Vector2 direction;
    Collider2D[] enemies_hit;
    float rotateBox;


    // Start is called before the first frame update


    public void Aim()
    {
        direction = (Playerposition - (Vector2)transform.position).normalized;

        transform.right = direction;

        Vector2 scale = transform.localScale;
        if (direction.x < 0)
        {
            scale.y = -1;
        }
        else if (direction.x > 0)
        {
            scale.y = 1;
        }
        transform.localScale = scale;
    }

    public void HitPlayer()
    {
        
        if (direction.y>=0) {
             rotateBox = Vector2.Angle(direction, Vector2.right);
        }
        if (direction.y < 0)
        {
            rotateBox = 180-Vector2.Angle(direction, Vector2.right);
        }
        Debug.Log(rotateBox);
        Vector2 hitbox = new Vector2(attackRangeX, attackRangeY);
        enemies_hit = Physics2D.OverlapBoxAll(attackarea.position, hitbox, rotateBox, PlayerLayer);
    }

    public void AttackMethod(float damage)
    {
 
        if (enemies_hit.Length > 0)
        {

            
            Unit enemy_hit = enemies_hit[0].GetComponent<Unit>();

            //take damage method has to come last because of damage number
            //Floating_Text damagenumber = enemy_hit.GetComponentInChildren<Floating_Text>();
            if (enemy_hit.name!="Moona Hoshinova") {
                enemy_hit.takeDamage(damage);
            }

        }

        
    }

    public static void DrawAttackArea(Vector2 topright,Vector2 bottomleft,Vector2 topleft,Vector2 bottomright)
    {
        

        Gizmos.DrawLine(topright,topleft);
        Gizmos.DrawLine(topleft, bottomleft);
        Gizmos.DrawLine(topright, bottomright);
        Gizmos.DrawLine(bottomright, bottomleft);
    }

    

}
