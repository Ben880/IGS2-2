using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PZ_Nodes : MonoBehaviour
{
    
    List<PZ_Node> nodes = new List<PZ_Node>();

    public enum NodeFindType
    {
        closestToPlayer,
        random,
        firstInList
    }
    //========================================================
    //============= Accessing Variables  =====================
    //========================================================
    public bool hasNodes()
    {
        return nodes.Count > 0;
    }
    public void returnNode(PZ_Node node)
    {
        nodes.Add(node);
    }
    public PZ_Node recieveNode(NodeFindType type)
    {
        switch (type)
        {
            case NodeFindType.random:
                return recieveNodeAt(Random.Range(0, nodes.Count));
            case NodeFindType.closestToPlayer:
                throw new NotImplementedException();
                break;
            default:
                return recieveNodeAt(0);
        }
    }
    private PZ_Node recieveNodeAt(int i)
    {
        PZ_Node tmpNode = nodes[i];
        nodes.RemoveAt(i);
        return tmpNode;
    }
    //========================================================
    //============= Unity Functions  =========================
    //========================================================
    void Awake()
    {
        foreach (var node in GameObject.FindGameObjectsWithTag("PuzzleNode"))
        {
            PZ_Node tmp = node.GetComponent<PZ_Node>();
            if (tmp.getNodeType() == PZ_NodeType.Puzzle)
                nodes.Add(tmp);
        }
    }
    void Update()
    {
        
    }
}
