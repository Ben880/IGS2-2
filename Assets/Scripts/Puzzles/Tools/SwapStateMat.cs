using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapStateMat : MonoBehaviour
{
    [SerializeField] 
    private Material matTrue;
    [SerializeField] 
    private Material matFalse;
    public bool startingState;
    private bool state;
    private Renderer renderer;
    [SerializeField] 
    private string key = "key";

    public string Key { get { return key; } }
    public bool State { get { return state;}}
    
    public void swapState()
    {
        setState(!state);
        
    }

    public void setState(bool b)
    {
        state = b;
        if (state)
            renderer.material = matTrue;
        else
            renderer.material = matFalse;
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        state = startingState;
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
