using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class PZ_Simon : PZ_Base
{
    //========================================================
    //================== Variables  ==========================
    //========================================================
    private List<SwapStateMat> states = new List<SwapStateMat>();
    private SwapStateMat solvedState;
    private SwapStateMat unsolvedState;
    private TimerCountdown resetPuzzleTimer;
    //serialized
    [SerializeField] 
    private float resetPuzzleTime = 4;
    //========================================================
    //============= Unity Functions  =========================
    //========================================================
    void Start()
    {
        baseStart();
        resetPuzzleTimer = new TimerCountdown(resetPuzzleTime);
        states = GetComponentsInChildren<SwapStateMat>().ToList();
        foreach (var state in states)
        {
            if (state.Key.Equals("Solved"))
                solvedState = state;
            if (state.Key.Equals("Unsolved"))
                unsolvedState = state;
        }
        updatePuzzleDisplay();
        buttonDictionary.Add("Red", 0);
        buttonDictionary.Add("Green", 1);
        buttonDictionary.Add("Blue", 2);
        buttonDictionary.Add("Yellow", 3);
    }
    void Update()
    {
        resetPuzzleTimer.Update();
        if (resetPuzzleTimer.isComplete())
        {
            
            if (entered.Count != 0)
            {
                entered.Clear();
                playIncorrectSound();
            }
        }
    }

    //========================================================
    //================== Overrides  ==========================
    //========================================================
    public override void userInput(string input)
    {
        resetPuzzleTimer.reset();
        parseUserInput(input);
        updatePuzzleDisplay();
        checkSolved();
        Debug.Log("User Input" + input + "entered " + entered.ToString());
        
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
        solveString = getClueString();
    }
    
    public override void updatePuzzleDisplay()
    {
        solvedState.setState(solved);
        unsolvedState.setState(!solved);
    }

    public override string getClueString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (int num in  solveCondition)
        {
            switch (num)
            {
                case 0:
                    sb.Append("R");
                    break;
                case 1:
                    sb.Append("G");
                    break;
                case 2:
                    sb.Append("B");
                    break;
                case 3:
                    sb.Append("Y");
                    break;
            }
        }
        return sb.ToString();
    }
    //========================================================
    //=================== default ============================
    //========================================================
    void LateUpdate()
    {
        if (solveCondition.Count == 0)
        {
            
            solveCondition.Add(0);
            solveCondition.Add(1);
            solveCondition.Add(2);
            Debug.Log("LateUpdate reached with no puzzle setting default"+ getClueString(), this);
        }
    }
}
