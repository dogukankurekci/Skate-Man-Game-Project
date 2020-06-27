using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager_DH : MonoBehaviour
{
    public Sounds[] sounds;

    void Awake()
    {
        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    public void Play(string name)
    {
        if (PlayerPrefs.GetInt("CurrentSFXBool") == 1)
        {
            Sounds s = Array.Find(sounds, sound => sound.name == name);
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
