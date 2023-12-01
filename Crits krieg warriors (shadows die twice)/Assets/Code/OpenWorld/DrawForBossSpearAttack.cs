using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawForBossSpearAttack : MonoBehaviour
{
    //
    public Transform top_right_corner;
    public Transform bottom_left_corner;
    public Transform bottom_right_corner;
    public Transform top_left_corner;

    private void OnDrawGizmos()
    {
        Boss_Moona_WeaponParent.DrawAttackArea(top_right_corner.position, bottom_left_corner.position, top_left_corner.position, bottom_right_corner.position);
    }



}
