using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public float percent = 1;

    public void setpercent(float a)
    {
        percent = a;
    }

    void Awake()
    {
            foreach (Sound s in sounds)
            {

                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;

                s.source.volume = s.volume * percent;
                s.source.loop = s.loop;

            }
        
    }

    
    public void percentUpdater()
    {
        foreach (Sound s in sounds)
        {

            s.source.volume = s.volume * percent;
        }
    }

    public void Play(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Play();
                return;
            }
        }
        Debug.LogWarning("No sounds found with name "+name);
        return;
    }

    public void Stop(string name)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == name)
            {
                s.source.Stop();
                return;
            }
        }
        Debug.LogWarning("No sounds found with name " + name);
        return;
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
                s.source.Stop();
                return;
        }
    }

}
