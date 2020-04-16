using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PZ_ZoneIdentifier : MonoBehaviour
{
    
    [SerializeField]
    private string name = "";
    [SerializeField]
    private int value = 0;

    public virtual int getValue()
    {
        return value;
    }

    public virtual string getName()
    {
        return name;
    }
}
