using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("BGM_begin");
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game!");
        FindObjectOfType<AudioManager>().StopAll();
        Application.Quit();
    }
}
