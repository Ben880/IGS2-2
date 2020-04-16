using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Linq;

public class PZ_Master : MonoBehaviour
{
    private PZ_Registry registry;
    private PZ_Nodes nodes;
    private List<PZ_Node> createdNodes = new List<PZ_Node>();
    private bool started = false;
    
    //========================================================
    //================== Logic  ==============================
    //========================================================
    public void nextNode()
    {
        generateNode();
        setPreviousClue();
        if (nodes.hasNodes())
        {
            getCurrentReward().NodeObject = Instantiate(getRClue(current()), getCurrentReward().transform); 
            nextNode();
        }
    }

    private void generateNode()
    {
        PZ_Node newNode = nodes.recieveNode(PZ_Nodes.NodeFindType.random);
        newNode.NodeObject = Instantiate(getRPuzzle(), newNode.gameObject.transform);
        getNodePZB(newNode).generateCondition();
        createdNodes.Add(newNode);
    }

    private void setPreviousClue()
    {
        getPriorRewardPZBase().SolveString = getCurrentPZB().getClueString();
        Debug.Log("Current puzzle string: " +getCurrentPZB().getClueString());
        getPriorRewardPZBase().updatePuzzleDisplay();
    }
    //========================================================
    //============= Unity Functions  =========================
    //========================================================
    void Start()
    {
        //getting objects
        registry = gameObject.GetComponent<PZ_Registry>();
        nodes = gameObject.GetComponent<PZ_Nodes>();
        // create a new node
        generateNode();
        getStartClue().NodeObject = (Instantiate(registry.getRandomClue(), getStartClue().transform));
        getCurrentReward().NodeObject = (Instantiate(registry.getRandomClue(), getCurrentReward().transform));
        setPreviousClue();
        nextNode();
    }
    //========================================================
    //============= Unit Logic  =========================
    //========================================================

    private PZ_Type getPreviousClueType()
    {
        return createdNodes.Last().getRewardNode().getNodeObjectInputType();
    }
    
    private PZ_Base getPZB(GameObject go)
    {
        return go.GetComponent<PZ_Base>();
    }

    private PZ_Base getPriorRewardPZBase()
    {
        if (createdNodes.Count > 1)
            return getPZB(createdNodes[createdNodes.Count - 2].getRewardNode().NodeObject);
        else
            return getNodePZB(getStartClue());
    }

    private PZ_Node current()
    {
        return createdNodes.Last();
    }

    private PZ_Node getCurrentReward()
    {
        return current().getRewardNode();
    }

    private PZ_Base getCurrentPZB()
    {
        return getNodePZB(current());
    }

    private PZ_Base getNodePZB(PZ_Node node)
    {
        return getPZB(node.NodeObject);
    }

    private PZ_Node getStartClue()
    {
        return registry.getStartClueNode();
    }

    private GameObject getRPuzzle()
    {
        if (createdNodes.Count > 0)
            return registry.getRandomPuzzleOfType(getPreviousClueType());
        else
            return registry.getRandomPuzzle();
    }

    private GameObject getRClue(PZ_Node node)
    {
        if (createdNodes.Count > 0)
            return registry.getRandomClueOfType(node.getNodeObjectInputType());
        else
            return registry.getRandomPuzzle();
    }
}
