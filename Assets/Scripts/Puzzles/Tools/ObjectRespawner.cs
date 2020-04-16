using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private string objectLeaveName = "Cube";
    TimerCountdown timer = new TimerCountdown(1);

    void Start()
    {
        var tmp = Instantiate(spawnObject, transform);
        tmp.transform.position = transform.position;
    }

    private void Update()
    {
        if (!timer.isComplete())
        {
            timer.Update();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.name.Equals(objectLeaveName) && timer.isComplete())
        {
            timer.reset();
            var tmp = Instantiate(spawnObject, transform);
            tmp.transform.position = transform.position;
        }
    }
    
}
