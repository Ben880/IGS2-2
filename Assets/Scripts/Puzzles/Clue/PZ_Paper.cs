using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PZ_Paper : PZ_Base
{
    public string testString;
    private TextMeshPro tmp;

    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshPro>();
        updatePuzzleDisplay();
    }

    public override void updatePuzzleDisplay()
    {
        //Debug.Log("Updat puzzle display called on paper, current solvestring:" + SolveString);
        if (SolveString != null)
            tmp.text = SolveString;
        else
        {
            tmp.text = testString;
        }
    }
}
