using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PZ_Node : MonoBehaviour
{

    [SerializeField]
    private PZ_NodeType type = PZ_NodeType.Puzzle;
    [SerializeField]
    private GameObject rewardNodeObject;
    private GameObject nodeObject;
    
    //========================================================
    //============= Accessing Variables  =====================
    //========================================================
    public GameObject NodeObject
    {
        get { return nodeObject;}
        set { nodeObject = value; }
    }
    
    public PZ_Type getNodeObjectInputType()
    {
        return nodeObject.GetComponent<PZ_Base>().InputType;
    }

    public PZ_NodeType getNodeType()
    {
        return type;
    }

    public GameObject getRewardNodeObject()
    {
        return rewardNodeObject;
    }
    public PZ_Node getRewardNode()
    {
        if (PZ_NodeType.Puzzle != type)
            Debug.Log("asking for a reward on an non puzzle type");
        return rewardNodeObject.GetComponent<PZ_Node>();
    }
    
    //========================================================
    //================== Logic  ==============================
    //========================================================

    
    //========================================================
    //============= Unity Functions  =========================
    //========================================================
    void Awake()
    {
        #if UNITY_EDITOR
        checkSetup();
        #endif
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //========================================================
    //================== Check  ==============================
    //========================================================
    private void checkSetup()
    {
        if (!tag.Equals("PuzzleNode"))
            Debug.LogError("Node script attached to a GameObject without tag: Node", gameObject);
        if (type == PZ_NodeType.Puzzle && rewardNodeObject == null)
            Debug.LogError("Node script marked as a puzzle and has no reward node", gameObject);
    }
}
