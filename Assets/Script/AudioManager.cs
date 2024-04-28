using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource effectSource;

    public AudioClip background;

    public static AudioManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<AudioManager>();
            }

            return m_instance;
        }
    }
    public static AudioManager m_instance;

    void Start()
    {
        musicSource.clip = background;  
        musicSource.loop = true;
        musicSource.Play();
    }

    public void effectPlay(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}
