using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public GameObject Player;

    public Transform Reset_area;

    public void Awake()
    {
        Player = FindObjectOfType<Player_Movement>().gameObject;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {

            Player.transform.position = Reset_area.position;

        }
    }
}
