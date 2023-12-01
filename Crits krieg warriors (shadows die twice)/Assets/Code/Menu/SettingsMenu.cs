using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioManager allsounds_level;

    public AudioManager allsounds_main_menu;

    public AudioManager allsounds_Open_world;

    

    public void Setvolume(float volume)
    {
        
        allsounds_level.setpercent(volume);
        allsounds_main_menu.setpercent(volume);
        allsounds_Open_world.setpercent(volume);
    }

    
}
