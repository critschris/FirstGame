using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    CheckPoint checkPoint;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM_begin");
        Time.timeScale = 1;
    }


    public void PlayGame()
    {
        if (!checkPoint.FinishedTutorial) { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        FindObjectOfType<AudioManager>().StopAll();
        Application.Quit();
    }
}
