using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PZ_Numpad : PZ_Base
{
    [SerializeField]
    private TextMeshPro displayMesh;

    
    void Start()
    {
        baseStart();
        updatePuzzleDisplay();
    }

    //========================================================
    //================== Overrides  ==========================
    //========================================================
    public override void checkSolved()
    {
        if (!solved && genericSolveTest())
        {
            solved = true;
            playCorrectSound();
        }
        else
        {
            playIncorrectSound();
        }
        
    }
    public override void generateCondition()
    {
        solveCondition = getConditionInFormat(puzzleLength, 0, 10);
        StringBuilder sb = new StringBuilder();
        foreach (int num in solveCondition)
        {
            sb.Append(num.ToString());
        }
        solveString = sb.ToString();
    }

    public override void updatePuzzleDisplay()
    {
        StringBuilder sb = new StringBuilder();
        while (entered.Count > puzzleLength)
        {
            entered.RemoveAt(entered.Count-1);
        }
        foreach (var val in entered)
        {
            sb.Append(val);
        }
        displayMesh.text = sb.ToString();
    }

    public void buttonPress(string s)
    {
        switch (s)
        {
            case "x":
                entered.Clear();
                break;
            case "o":
                checkSolved();
                break;
            case "0":
                entered.Add(0);
                break;
            case "1":
                entered.Add(1);
                break;
            default:
                int i = int.Parse(s);
                entered.Add(i);
                break;
        }
        updatePuzzleDisplay();
    }
    
    void LateUpdate()
    {
        if (solveCondition.Count == 0)
        {
            Debug.Log("LateUpdate reached with no puzzle setting default 012", this);
            solveCondition.Add(0);
            solveCondition.Add(1);
            solveCondition.Add(2);
        }
    }
    
}
