using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PZ_GenericText : PZ_Base
{
    public string testString;
    public string unsolvedString = "Awaiting input"; 
    private TextMeshPro tmp;

    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        updatePuzzleDisplay();
    }

    public override void updatePuzzleDisplay()
    {
        if (solved)
        {
            if (SolveString != null)
                tmp.text = SolveString;
            else
            {
                tmp.text = testString;
            }
        }
        else
        {
            tmp.text = unsolvedString;
        }
    }
}
