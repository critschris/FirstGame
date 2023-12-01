using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Sound 
{
    public AudioClip clip;

    public string name;

    [Range(0f,1f)]
    public float volume;

    [HideInInspector]
    public float percent;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public void setvolumepercent(float a)
    {
        volume = a*percent;
    }


}
