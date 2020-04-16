using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class PZ_Scale : PZ_Base
{
    [SerializeField]
    private TextMeshPro displayMesh;
    [SerializeField] private int tensMax = 2;
    [SerializeField] private int onesMax = 3;
    [SerializeField] private int tenthsMax = 4;
    [SerializeField] private int hundredthsMax = 5;

    
    void Start()
    {
        baseStart();
        for (int i = 0; i < puzzleLength; i++)
        {
            entered.Add(0);
        }
        updatePuzzleDisplay();
    }

    //========================================================
    //================== Overrides  ==========================
    //========================================================

    public override void generateCondition()
    {
        if (puzzleLength > 4)
            puzzleLength = 4;
        List<int> temp = new List<int>();
        if (puzzleLength> 0)
            temp.Add(UnityEngine.Random.Range(0, tensMax));
        if (puzzleLength> 1)
            temp.Add(UnityEngine.Random.Range(0, onesMax));
        if (puzzleLength> 2)
            temp.Add(UnityEngine.Random.Range(0, tenthsMax));
        if (puzzleLength> 3)
            temp.Add(UnityEngine.Random.Range(0, hundredthsMax));
        solveCondition = temp;
        solveString = genericSolveString().Insert(1, ".");
    }

    public override void updatePuzzleDisplay()
    {
        StringBuilder sb = new StringBuilder();
        bool addPeriod = true;
        foreach (var val in entered)
        {
            sb.Append(val);
            if (addPeriod)
            {
                sb.Append(".");
                addPeriod = false;
            }
        }
        displayMesh.text = sb.ToString();
    }
    
    public override void userInput(int input)
    {
        int counter = 0;
        for (int i = 1000; i >= 1; i /= 10)
        {
            if (Math.Abs(input) == i)
            {
                entered[counter] += input / i;
            }
            counter++;
        }
        updatePuzzleDisplay();
        checkSolved();
    }

    protected override void defaultGenerate()
    {
        solveCondition.Add(0);
        solveCondition.Add(1);
        solveCondition.Add(2);
        solveCondition.Add(3);
    }
}
