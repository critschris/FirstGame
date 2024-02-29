using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonSceneActivator : MonoBehaviour
{

    public GameObject Camera;

    public Camera Cam;

    public GameObject mapPreFab;

    public Transform Dungeonarea;

    public GameObject Player;

    public Animator Fade;

    public bool once =true;

    public float size;

    public GameObject DungeonInfo;

    private void Start()
    {
        Cam = Camera.GetComponent<Camera>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player"&&once)
        {
            Debug.Log("Activated cutscene");

            once = false;
            StartCoroutine(Dialog());
            
        }
    }

    IEnumerator Dialog()
    {
        Instantiate(mapPreFab,Dungeonarea.position,Quaternion.identity);
        FindObjectOfType<AudioManager>().Stop("BGM");
        FindObjectOfType<Happen>().SavePlayerStats();
        FindObjectOfType<Happen>().LoadStatsForDungeon();
        yield return new WaitForSeconds(3);
        Fade.SetTrigger("Fade");
        yield return new WaitForSeconds(1);
        Player.transform.position = new Vector3(0, 47F, 0);
        Camera.GetComponent<DungeonCam>().SetUp();
        Camera.transform.position = new Vector3(0, 47F, -10);
        yield return new WaitForSeconds(1);
        size = Cam.orthographicSize;
        while(Cam.orthographicSize<9){
            Cam.orthographicSize += 0.5F;
            yield return new WaitForSeconds(0.05F);
        }
        FindObjectOfType<Happen>().SetTimeScaletoZero();
        DungeonInfo.SetActive(true);

        //StartCoroutine(Camera.GetComponent<DungeonCam>().Shake(1,0.5F));

    }
    public void triggerFade()
    {
        Fade.SetTrigger("Fade");
    }

    public void camerasizereset()
    {
        Cam.orthographicSize = size;
    }
    
}
