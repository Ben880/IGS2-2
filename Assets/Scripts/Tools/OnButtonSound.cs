using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(AudioSource))]
public class OnButtonSound : MonoBehaviour
{
    private AudioSource ac;

    void Start()
    {
        ac = GetComponent<AudioSource>();
        GetComponent<HoverButton>().onButtonDown.AddListener(playSound);
    }

    public void playSound(Hand h)
    {
        ac.Stop();
        ac.Play();
    }
    
}
