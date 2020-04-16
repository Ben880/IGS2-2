using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_ClueBase : MonoBehaviour
{
    public PZ_Type inputType; 
    public dynamic input;
    //========================================================
    //============= Accessing Variables  =====================
    //========================================================
    public void setInput(dynamic variable)
    {
        input = variable;
    }
    public dynamic getInput()
    {
        return input;
    }
    public PZ_Type getInputType()
    {
        return inputType;
    }
    //========================================================
    //============= Unity Functions  =========================
    //========================================================
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
