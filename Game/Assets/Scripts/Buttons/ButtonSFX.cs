using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
   // private AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    // 

    public void HoverSound()
    {
        AudioPlayer.instance.GetComponent<AudioSource>().PlayOneShot(hoverFX);
    }
    public void ClickSound()
    {
        AudioPlayer.instance.GetComponent<AudioSource>().PlayOneShot(clickFX);
    }
    //
}
