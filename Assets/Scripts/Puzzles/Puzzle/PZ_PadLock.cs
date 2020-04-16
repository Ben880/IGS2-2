using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class PZ_PadLock : PZ_Base
{
    //========================================================
    //================== Variables  ==========================
    //========================================================
    private List<SwapStateMat> states = new List<SwapStateMat>();
    private SwapStateMat solvedState;
    private SwapStateMat unsolvedState;
    Dictionary<string, PadBSwaper> buttonScripts = new Dictionary<string, PadBSwaper>();

    //========================================================
    //============= Unity Functions  =========================
    //========================================================

    void Start()
    {
        baseStart();
        states = GetComponentsInChildren<SwapStateMat>().ToList();
        foreach (var state in states)
        {
            if (state.Key.Equals("Solved"))
                solvedState = state;
            if (state.Key.Equals("Unsolved"))
                unsolvedState = state;
        }
        updatePuzzleDisplay();
        buttonDictionary.Add("D0", 0);
        buttonDictionary.Add("D1", 1);
        buttonDictionary.Add("D2", 2);
        buttonDictionary.Add("D3", 3);
        List<PadBSwaper> temp = GetComponentsInChildren<PadBSwaper>().ToList();
        foreach (PadBSwaper pad in temp)
        {
            buttonScripts.Add("D" + pad.index, pad);
        }
    }
    
    void Update()
    {

 

    }

    //========================================================
    //================== Overrides  ==========================
    //========================================================
    public override void userInput(string input)
    {
        buttonScripts["D"+input].buttonPress();
        entered.Clear();
        for (int i = 0; i < buttonScripts.Count; i++)
        {
            entered.Add(buttonScripts["D"+i].getCurrent());
        }
        updatePuzzleDisplay();
        checkSolved();
    }

    public override void checkSolved()
    {
        if (!solved && genericSolveTest())
        {
            solved = true;
            playCorrectSound();
            updatePuzzleDisplay();
        }
        
    }
    public override void generateCondition()
    {
        solveCondition = getConditionInFormat(puzzleLength, 0, 4);
        StringBuilder sb = new StringBuilder();
        foreach (int num in solveCondition)
        {
            sb.Append(num.ToString());
        }
        solveString = sb.ToString();
    }
    
    public override void updatePuzzleDisplay()
    {
        solvedState.setState(solved);
        unsolvedState.setState(!solved);
    }

    public virtual string getClueString()
    {
        return solveString;
    }
    //========================================================
    //=================== default ============================
    //========================================================
    void LateUpdate()
    {
        if (solveCondition.Count == 0)
        {
            Debug.Log("LateUpdate reached with no puzzle setting default 0123", this);
            solveCondition.Add(0);
            solveCondition.Add(1);
            solveCondition.Add(2);
            solveCondition.Add(3);
        }
    }
}
