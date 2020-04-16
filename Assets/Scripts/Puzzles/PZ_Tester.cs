using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Tester : MonoBehaviour
{
    private PZ_Base baseClass;
    // Start is called before the first frame update
    void Start()
    {
        baseClass = gameObject.GetComponent<PZ_Base>();
        testType("String");
        testType(2);
        testType((double) 2.3);
        testType((float) 2.4);
        testType('e');
    }

    private void testType(dynamic variable)
    {
        //baseClass.setInput(variable);
        log(baseClass);
    }
    private void log(PZ_Base baseClass)
    {
        //Debug.Log(baseClass.getInput());
        //Debug.Log(baseClass.getInputType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
