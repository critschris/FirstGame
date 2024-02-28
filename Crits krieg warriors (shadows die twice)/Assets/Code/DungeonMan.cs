using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMan : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public Transform[] Spawnpoints;

    public Animator FloatingE_Spawner;
    public Happen Eventsystem;



    public GameObject Player;

    int Stagecounter = 0;

    int finalstage = 7;

    public int CheckerWidth;
    public int CheckerLength;

    private void Awake()
    {
        Eventsystem = FindObjectOfType<Happen>();
        Player = FindObjectOfType<Player_Movement>().gameObject;
    }

    private void Update()
    {
        
        if (Stagecounter < finalstage && FloatingE_Spawner.GetBool("Appear")&&Input.GetKeyDown(KeyCode.E))
        {
            randomSpawner();
            Stagecounter++;
            return;
        }
        if (Stagecounter == finalstage && EmptyChecker())
        {
            StartCoroutine(End());
        }

    }

    public bool EmptyChecker()
    {
        Collider2D[] Checker = Physics2D.OverlapBoxAll(transform.position, new Vector2(CheckerWidth,CheckerLength),0);
        if (Checker.Length==1)
        {
            return true;
        }
        return false;
    }

    public void randomSpawner()
    {
        for (int i = 0; i < Spawnpoints.Length; i++){

            int random = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[random],Spawnpoints[i].position,Quaternion.identity);
        }
    }


    public IEnumerator End()
    {
        FindObjectOfType<DungeonSceneActivator>().triggerFade();
        
        yield return new WaitForSeconds(1);
        FindObjectOfType<DungeonCam>().ExitDungeon();
        Player.transform.position = new Vector3(1,5,0);
        FindObjectOfType<Happen>().LoadPlayerStatsOnPlayer();
        Eventsystem.ActivePillar();
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(CheckerWidth,CheckerLength,0));
    }
}
