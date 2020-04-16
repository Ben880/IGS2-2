using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PZ_ZoneInput : MonoBehaviour
{
    [SerializeField] 
    private PZ_Base basePZ;

    [SerializeField] 
    private string acceptIdentifier = "";
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        var otherIdent = other.GetComponent<PZ_ZoneIdentifier>();
        if (otherIdent != null && otherIdent.getName().Equals(acceptIdentifier))
        {
            basePZ.userInput((int) otherIdent.getValue());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var otherIdent = other.GetComponent<PZ_ZoneIdentifier>();
        if (otherIdent != null && otherIdent.getName().Equals(acceptIdentifier))
        {
            basePZ.userInput((int) otherIdent.getValue()*-1);
        }
    }

     
}
