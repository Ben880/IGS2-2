using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmmisiveControler : MonoBehaviour
{
    public Material defaultMaterial;
    public Material emmisiveMaterial;
    private TimerCountdown timer = new TimerCountdown(0.3f);
    private bool waitForTimer = false;
    private Renderer renderer;
    
    void Awake()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = defaultMaterial;
    }

    void Update()
    {
        if (waitForTimer)
        {
            timer.Update();
            if (timer.isComplete())
            {
                waitForTimer = false;
                renderer.material = defaultMaterial;
            }
        }
    }

    public void enableEmmision()
    {
        renderer.material = emmisiveMaterial;
    }

    public void disableEmmision()
    {
        timer.reset();
        waitForTimer = true;
    }


}