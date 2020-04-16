using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportOnCollide : MonoBehaviour
{
    
    public Vector3 teleportLocation = Vector3.zero;



    private void OnTriggerEnter(Collider other)
    {
        other.transform.position =teleportLocation;
        Rigidbody rb = other.transform.parent.gameObject.GetComponentInChildren<Rigidbody>();
        bool k = rb.isKinematic;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = k;
    }
}
