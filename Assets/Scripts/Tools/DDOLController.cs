using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DDOLController : MonoBehaviour
{
    [SerializeField]
    private string lookForTag;
    
    [SerializeField]
    private GameObject localPlayer;
    private GameObject locatedPlayer;
    private GameObject activePlayer;
    [SerializeField]
    private Vector3 offsetPosition;

    private bool exec = false;
    void Awake()
    {
        localPlayer.SetActive(false);
        locatedPlayer = GameObject.FindGameObjectWithTag(lookForTag);
        if (locatedPlayer == null)
        {
            localPlayer.SetActive(true);
            activePlayer = localPlayer;
        }
        else
        {
            activePlayer = locatedPlayer;
        }
        //Valve.VR.OpenVR.System.ResetSeatedZeroPose();
        Valve.VR.OpenVR.Compositor.SetTrackingSpace(Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);
        UnityEngine.XR.InputTracking.Recenter();
        activePlayer.transform.position = this.transform.position;
        //activePlayer.transform.position = offsetPosition;
        activePlayer.transform.position -= gameObject.transform.position- GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        Debug.Log("DDOLC reached end awake", this);
    }

    void LateUpdate()
    {
        if (!exec)
        {
            Vector3 tmp = gameObject.transform.position - GameObject.FindGameObjectWithTag("MainCamera").transform.position;
            tmp.y = 0;
            activePlayer.transform.position += tmp;
            exec = true;
        }
    }


}
