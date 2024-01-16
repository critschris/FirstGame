using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingInAttack : MonoBehaviour
{
    public float displacement;
    public float SizeHorizontal;
    public float SizeVertical;
    
    public Transform AreaToTheLeft;
    public Transform AreaToTheRight;
    public Transform AreaToTheTop;
    public Transform AreaToTheBottom;
    public LayerMask PlayerLayer;

    public bool hitPlayer=false;


    public void Catcher(Unit player)
    {

        
        //Debug.Log(temp);
        Vector2 hitbox = new Vector2(SizeHorizontal, SizeVertical);
        Collider2D[] enemies_hit_left = Physics2D.OverlapBoxAll(AreaToTheLeft.position, hitbox, 0, PlayerLayer);
        Collider2D[] enemies_hit_right = Physics2D.OverlapBoxAll(AreaToTheRight.position, hitbox, 0, PlayerLayer);
        Collider2D[] enemies_hit_top = Physics2D.OverlapBoxAll(AreaToTheTop.position, hitbox, 0, PlayerLayer);
        Collider2D[] enemies_hit_bottom = Physics2D.OverlapBoxAll(AreaToTheBottom.position, hitbox, 0, PlayerLayer);

        if ((enemies_hit_left.Length>0|| enemies_hit_right.Length>0|| enemies_hit_top.Length>0|| enemies_hit_bottom.Length>0))
        {
            Debug.Log(hitPlayer);
            hitPlayer = true;
            player.Stunned = true;
            
        }

    }

    public void SlamKwan(Unit player,float damage)
    {
        if (hitPlayer==true)
        {
            player.takeDamage(damage);
            hitPlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(AreaToTheLeft.position,new Vector2(SizeHorizontal, SizeVertical));
        Gizmos.DrawWireCube(AreaToTheRight.position, new Vector2(SizeHorizontal, SizeVertical));
        Gizmos.DrawWireCube(AreaToTheTop.position, new Vector2(SizeHorizontal, SizeVertical));
        Gizmos.DrawWireCube(AreaToTheBottom.position, new Vector2(SizeHorizontal, SizeVertical));
    }
}
