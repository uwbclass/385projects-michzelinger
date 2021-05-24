using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicControl : MonoBehaviour
{
    public AudioSource audioSource;
    private float musicVolume = 1f;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
