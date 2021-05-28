using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    private AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    void Awake()
    {
        myFX = AudioPlayer.instance.GetComponent<AudioSource>();
    }

    public void HoverSound()
    {
        myFX.PlayOneShot(hoverFX);
    }
    public void ClickSound()
    {
        myFX.PlayOneShot(clickFX);
    }
    //
}
