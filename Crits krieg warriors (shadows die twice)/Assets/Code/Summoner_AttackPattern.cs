using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summoner_AttackPattern : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;

    public GameObject SummonPrefab;

    public Transform Pivot;

    public Slider SummonerHealth;
    Unit SummonerUnit;

    // Start is called before the first frame update
    void Start()
    {
        SummonerUnit = GetComponent<Unit>();
        StartCoroutine(SummonerAttackPattern());
    }

    // Update is called once per frame
    void Update()
    {
        SummonerHealth.value = (SummonerUnit.cHP / SummonerUnit.maxHP);

        if (SummonerUnit.cHP < 0)
        {
            Destroy(gameObject);
        }
    }


    public IEnumerator SummonerAttackPattern()
    {

        while (true)
        {
            yield return new WaitForSeconds(2F);
            Aim();
            Summon();
            yield return new WaitForSeconds(13F);
        }

    }

    public void Summon()
    {
        Instantiate(SummonPrefab, spawn1.position, Quaternion.identity);
        Instantiate(SummonPrefab, spawn2.position, Quaternion.identity);
        Instantiate(SummonPrefab, spawn3.position, Quaternion.identity);
    }

    public void Aim()
    {
        Transform playerposition = FindObjectOfType<Player_Movement>().gameObject.transform;
        Vector2 direction = ((Vector2)playerposition.position - (Vector2)transform.position).normalized;
        Pivot.right = direction;
    }
}
