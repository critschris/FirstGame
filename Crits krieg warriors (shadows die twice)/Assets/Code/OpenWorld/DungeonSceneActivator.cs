using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonSceneActivator : MonoBehaviour
{

    public GameObject Camera;

    public GameObject mapPreFab;

    public Transform Dungeonarea;

    public GameObject Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Activated cutscene");
            StartCoroutine(Dialog());
            
        }
    }

    IEnumerator Dialog()
    {
        Instantiate(mapPreFab,Dungeonarea.position,Quaternion.identity);
        FindObjectOfType<AudioManager>().Stop("BGM");
        yield return new WaitForSeconds(3);
        Player.transform.position = new Vector3(0, 47F, 0);
        Camera.transform.position = new Vector3(0, 47F, 0);

    }
}
