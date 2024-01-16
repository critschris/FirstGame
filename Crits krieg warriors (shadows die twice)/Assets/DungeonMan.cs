using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMan : MonoBehaviour
{

    public Unit[] enemies;
    public Transform[] Spawnpoints;

    private void Awake()
    {
        enemies = new Unit[4];
    }

    private void Update()
    {
        if (EmptyChecker())
        {
            randomSpawner();
            return;
        }
    }

    public bool EmptyChecker()
    {
        for (int i = 0; i < enemies.Length; i++){
            if (enemies[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public void randomSpawner()
    {

    }

}
