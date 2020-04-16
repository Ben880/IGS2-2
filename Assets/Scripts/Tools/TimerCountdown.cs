using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountdown
{
    private float timer = 0;
    private float resetTime = 0;
    
    public TimerCountdown(float resetTime)
    {
        this.resetTime = resetTime;
    }
    
    public float ResetTime
    {
        set { resetTime = value; }
    }
    
    public void resetIfNotRunning()
    {
        if (isComplete())
            reset();
    }
    
    public void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            timer = 0;
        }
    }

    public void reset()
    {
        timer = resetTime;
    }

    public bool isComplete()
    {
        if (timer <= 0)
        {
            return true;
        }
        return false;
    }

}
