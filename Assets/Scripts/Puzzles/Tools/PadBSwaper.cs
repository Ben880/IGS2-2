using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadBSwaper : MonoBehaviour
{
    
    public int index; 
    private int current =0;
    private TextMeshPro tmp;
    

    public void buttonPress()
    {
        current++;
        current = (current > 9) ? 0 : current;
        tmp.text = current.ToString();
    }

    public int getCurrent()
    {
        return current;
    }
    // Start is called before the first frame update
    void Start()
    {
        tmp = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
