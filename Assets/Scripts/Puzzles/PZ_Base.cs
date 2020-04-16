using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PZ_Base : MonoBehaviour
{
    //========================================================
    //================== Variables  ==========================
    //========================================================
    [SerializeField]
    protected PZ_NodeType nodeType;
    [SerializeField]
    protected PZ_Type inputType;
    [SerializeField]
    protected int puzzleLength = 5;
    [SerializeField] 
    protected AudioClip correctSound;
    [SerializeField] 
    protected AudioClip incorrectSound;

    protected List<int> solveCondition = new List<int>();
    protected bool solved;
    protected List<int> entered = new List<int>();
    protected AudioSource audioSorce;
    protected String solveString;
    protected Dictionary<string, int> buttonDictionary = new Dictionary<string, int>();
    
    //========================================================
    //================== Unity Functions  ====================
    //========================================================
    void LateUpdate()
    {
        if (solveCondition.Count == 0)
        {
            defaultGenerate();
            Debug.Log("LateUpdate reached with no puzzle setting default" + genericSolveString());
        }
    }
    //========================================================
    //================== Get/Set  ============================
    //========================================================
    public string SolveString
    {
        get { return solveString; }
        set { solveString = value; }
    }
    public bool Solved { get{return solved;}}
    public int PuzzleLength
    {
        get { return puzzleLength; }
        set { puzzleLength = value; }
    }
    public List<int> SolveCondition
    {
        get { return solveCondition; }
        set { solveCondition = value; }
    }
    public PZ_Type InputType
    {
        get { return inputType; }
    }
    public PZ_NodeType NodeType
    {
        get { return nodeType; }
    }
    //========================================================
    //================== Virtual  ============================
    //========================================================
    public virtual void generateCondition()
    {
        Debug.Log("No set generate method returning default 5 count list 0-9");
        solveCondition = getConditionInFormat(5,0,10);
    }
    public virtual void updatePuzzleDisplay() { }
    public virtual void checkSolved() { genericCheckSolved(); }
    public virtual void userInput(string input) {  }
    public virtual void userInput(int input) { }
    public virtual string getClueString() { return solveString;}
    public virtual int getClueInteger() { return int.Parse(solveString);}
    protected virtual void defaultGenerate() { 
        solveCondition.Add(0);
        solveCondition.Add(1);
        solveCondition.Add(2); }
    //========================================================
    //================== Logic  ==============================
    //========================================================

    protected void baseStart()
    {
        audioSorce = gameObject.GetComponent<AudioSource>();
    }
    
    protected List<int> getConditionInFormat(int length, int minInclusive, int maxExclusive)
    {
        List<int> temp = new List<int>();
        for (int i = 0; i < length; i++)
        {
            temp.Add(UnityEngine.Random.Range(minInclusive, maxExclusive));
        }
        return temp;
    }

    protected string genericSolveString()
    {
        Debug.Log("genericSolveString()", this);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < solveCondition.Count; i++)
        {
            sb.Append(solveCondition[i].ToString());
            
        }
        return sb.ToString();
    }

    protected void genericCheckSolved()
    {
        Debug.Log("genericCheckSolved()", this);
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

    protected bool genericSolveTest()
    {
        Debug.Log("genericSolveTest()", this);
        if (entered.Count != solveCondition.Count)
        {
            Debug.Log("entered length differs from actual");
            return false;
        }
            
        for (int i = 0; i < entered.Count; i++)
        {
            Debug.Log($"Testing {entered[i]} and {solveCondition[i]}");
            if (entered[i] != solveCondition[i])
                return false;
        }
        return true;
    }

    protected void parseUserInput(string userInput)
    {
        if (buttonDictionary.ContainsKey(userInput))
            entered.Add(buttonDictionary[userInput]);
        else
            Debug.LogError("Unidentifiable input", this);
    }
    //========================================================
    //================== Sounds  =============================
    //========================================================
    protected void playCorrectSound()
    {
        audioSorce.Stop();
        audioSorce.clip = correctSound;
        audioSorce.Play();
    }
    
    protected void playIncorrectSound()
    {
        audioSorce.Stop();
        audioSorce.clip = incorrectSound;
        audioSorce.Play();
    }

    protected void playSound(AudioClip clip)
    {
        audioSorce.Stop();
        audioSorce.clip = clip;
        audioSorce.Play();
    }


    
}
