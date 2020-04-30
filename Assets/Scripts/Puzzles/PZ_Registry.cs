using System;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Registry : MonoBehaviour
{
    public List<GameObject> puzzleObjects = new List<GameObject>();
    public List<GameObject> clueObjects = new List<GameObject>();
    private Dictionary<PZ_Type, List<GameObject>> puzzleDict = new Dictionary<PZ_Type, List<GameObject>>();
    private Dictionary<PZ_Type, List<GameObject>> clueDict = new Dictionary<PZ_Type, List<GameObject>>();
    public GameObject startClueNode;
    
    //========================================================
    //============= Random Variables  =====================
    //=======================================================
    public GameObject getRandomPuzzle()
    {
        return getRandomPuzzleOfType(getRandomType());
    }
    public GameObject getRandomPuzzleOfType(PZ_Type type)
    {
        if (puzzleDict[type].Count == 0)
            repopulatePuzzleList(type);
        int tmp = UnityEngine.Random.Range(0, puzzleDict[type].Count);
        GameObject tmpObj = puzzleDict[type][tmp];
        puzzleDict[type].RemoveAt(tmp);
        return tmpObj;
    }
    public GameObject getRandomClue()
    {
        return clueObjects[UnityEngine.Random.Range(0, clueObjects.Count)];
    }
    public GameObject getRandomClueOfType(PZ_Type type)
    {
        return clueDict[type][UnityEngine.Random.Range(0, clueDict[type].Count)];
    }
    public PZ_Type getRandomType()
    {
        PZ_Type tmp = PZ_Type.String;
        bool invalid = true;
        while (invalid)
        {
            tmp = (PZ_Type) UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(PZ_Type)).Length);
            if (puzzleDict[tmp].Count > 0)
                invalid = false;
        }
        return (PZ_Type) tmp;
    }
    //========================================================
    //============= Accessing Variables  =====================
    //========================================================
    public PZ_Node getStartClueNode()
    {
        return startClueNode.GetComponent<PZ_Node>();
    }
    public PZ_Base getPZBase(GameObject obj)
    {
        return obj.GetComponent<PZ_Base>();
    }
    //========================================================
    //================== Logic  ==============================
    //========================================================
    public void sortCluesNPuzzles()
    {
        // generate lists
        foreach(PZ_Type type in Enum.GetValues(typeof(PZ_Type)))
        {
            puzzleDict.Add(type, new List<GameObject>());
            clueDict.Add(type, new List<GameObject>());
        }
        foreach (var puzzle in puzzleObjects)
        {
            puzzleDict[puzzle.GetComponent<PZ_Base>().InputType].Add(puzzle);
        }
        foreach (var clue in clueObjects)
        {
            clueDict[clue.GetComponent<PZ_Base>().InputType].Add(clue);
        }
    }
    private void repopulatePuzzleList(PZ_Type type)
    {
        foreach (var puzzle in puzzleObjects)
        {
            if (type == puzzle.GetComponent<PZ_Base>().InputType)
                puzzleDict[type].Add(puzzle);
        }
    }
    //========================================================
    //============= Unity Functions  =========================
    //========================================================
    void Awake()
    {
        sortCluesNPuzzles();
        #if UNITY_EDITOR
        checkSetup();
        #endif
    }
    //========================================================
    //================== Check  ==============================
    //========================================================
    private void checkSetup()
    {
        return;
        if (startClueNode == null)
            Debug.Log("No starting clue", gameObject);
        if (puzzleObjects.Count == 0)
            Debug.LogError("There are no puzzles", gameObject);
        else
            foreach (var item in puzzleDict)
            {
                if (item.Value.Count == 0)
                    Debug.LogError("There are no puzzles of type: " + item.Key, gameObject);
            }
        if (clueObjects.Count == 0)
            Debug.LogError("There are no clues", gameObject);
        else
            foreach (var item in clueDict)
            {
                if (item.Value.Count == 0)
                    Debug.LogError("There are no clues of type: " + item.Key, gameObject);
            }
        foreach (GameObject obj in puzzleObjects)
        {
            if (obj.GetComponent<PZ_Base>().NodeType != PZ_NodeType.Puzzle)
                Debug.LogError("There is a non puzzle object listed as a puzzle: " + obj.name, gameObject);
        }
        foreach (GameObject obj in clueObjects)
        {
            if (obj.GetComponent<PZ_Base>().NodeType != PZ_NodeType.Clue)
                Debug.LogError("There is a non clue object listed as a clue: " + obj.name, gameObject);
        }
    }
}
