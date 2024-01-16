using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonaFireWeaponParent : MonoBehaviour
{
    public Transform firepoint;
    public GameObject Projectileprefab;
    public Transform playerposition;
    public Transform Moonaposition;

    public void Start()
    {
        //StartCoroutine(FireTest());
    }

    IEnumerator FireTest()
    {
        while (true)
        {
            yield return new WaitForSeconds(5F);
            Aim();
            Fire();
        }

    }

    public void Aim()
    {
        Vector2 direction = ((Vector2)playerposition.position - (Vector2)transform.position).normalized;
        transform.right = direction;
    }

    public void Fire(){
        Instantiate(Projectileprefab,firepoint.position,firepoint.rotation);
    }

    
}
