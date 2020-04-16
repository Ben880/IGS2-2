using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TMP_Random : MonoBehaviour
{
    private enum UseMesh
    {
        TMPUGUI = 0,
        TMP = 1,
        TM =2,
        NONE = 3
    }
    private UseMesh useMesh = UseMesh.NONE;
    private TextMeshProUGUI tmpugui;
    private TextMeshPro tmp;
    private TextMesh tm;
    private TimerCountdown count = new TimerCountdown(1);
    private List<string> list = new List<string>();
    private const string lowerPool = "abcdefghijklmnopqrstuvwxyz";
    private const string upperPool = "QWERTYUIOPASDFGHJKLZXCVBNM";
    private const string numberPool = "0123456789";
    private string mergedPool;

    public bool lower = true;
    public bool upper = true;
    public bool numbers = true;
    public bool mergePools;
    public List<string> pools = new List<string>();
    public string header ="This is not a clue...YET ;D";
    public List<string> prefixList = new List<string>();
    public List<string> suffixList = new List<string>();
    public int lines = 7;
    public int minLength = 20;
    public int length = 20;
    public float updateTime = 1f;
    
    //bursting
    public bool useBurst = false;
    [Range(0f,100f)]
    public float burstChanceSec;
    public int burstMin = 1;
    public int burstMax = 6;
    public bool burstOverTime = true;
    public float burstTime;
    [Range(1f, 20f)]
    public float randomDeviationPercent = 10;
    
    private int burstLinesLeft = 0;
    private TimerCountdown burstCheck = new TimerCountdown(1);
    private TimerCountdown burstCountdown = new TimerCountdown(1);
    private bool bursting = false;
    private float burstInterval = 0;
    
    
    void Start()
    {
        tmpugui = GetComponent<TextMeshProUGUI>();
        tmp = GetComponent<TextMeshPro>();
        tm = GetComponent<TextMesh>();
        if (tmpugui != null)
            useMesh = UseMesh.TMPUGUI;
        else if (tmp != null)
            useMesh = UseMesh.TMP;
        else if (tm != null)
            useMesh = UseMesh.TM;
        else
            Debug.LogError("No TextMeshProUGUI, TextMeshPro, or TextMesh attached to object", this);
        count.ResetTime = updateTime;
        count.reset();
        if (lower)
            pools.Add(lowerPool);
        if (upper)
            pools.Add(upperPool);
        if (numbers)
            pools.Add(numberPool);
        if (mergePools)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string pool in pools)
            {
                sb.Append(pool);
            }
            mergedPool = sb.ToString();
        }
        //removed due to the case of the phantom break line
        //header += "\n";
    }
    
    void Update()
    {
        count.Update();
        if (count.isComplete())
        {
            count.reset();
            addLines(1);
        }
        if (useBurst)
        {
            burstCheck.Update();
            if (burstCheck.isComplete())
            {
                burstCheck.reset();
                if (UnityEngine.Random.Range(0,100) < burstChanceSec)
                    burstSetup();
            }
            if (bursting)
            {
                burstUpdate();
                burstCheck.reset();
            }
        }
    }
    private void addLines(int count)
    {
        for (int i = 0; i < count; i++)
            list.Insert(0, GenerateString(UnityEngine.Random.Range(minLength,length)));
        trimToLength();
        updateText();
    }
    
    private void burstSetup()
    {
        if (burstOverTime)
        {
            bursting = true;
            burstLinesLeft = UnityEngine.Random.Range(burstMin, burstMax);
            addBurstLine();
            burstInterval = burstTime / burstLinesLeft;
            nextBurstCountdown();
        }
        else
            addLines(UnityEngine.Random.Range(burstMin, burstMax));
    }
        
    private void burstUpdate()
    {
        burstCountdown.Update();
        if (burstCountdown.isComplete())
        {
            addBurstLine();
            nextBurstCountdown();
        }
    }
    
    private void nextBurstCountdown()
    {
        float deveation =UnityEngine.Random.Range(-randomDeviationPercent, randomDeviationPercent);
        deveation = deveation>0 ? (deveation/100) +1:1-((deveation*-1)/100);
        burstCountdown.ResetTime = burstInterval * deveation;
        burstCountdown.reset();
    }
    
    private void addBurstLine()
    {
        burstLinesLeft--;
        addLines(1);
        bursting = burstLinesLeft != 0;
    }

    
    private void updateText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(header);
        foreach (string str in list)
            sb.Append(str);
        switch (useMesh)
        {
            case UseMesh.TM:
                tm.text = sb.ToString();
                break;
            case UseMesh.TMP:
                tmp.text = sb.ToString();
                break;
            case UseMesh.TMPUGUI:
                tmpugui.text = sb.ToString();
                break;
            case UseMesh.NONE:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void trimToLength()
    {
        while (list.Count >= lines)
            list.RemoveAt(list.Count-1);
    }
    
    private string GenerateString(int length)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(getPrefix());
        for (int i = 0; i < length; i++)
        {
            sb.Append(generateChar());
        }
        sb.Append(getSuffix());
        sb.Append("\n");
        return sb.ToString();
    }

    private char generateChar()
    {
        if (mergePools)
        {
            int poolChar = UnityEngine.Random.Range(0, mergedPool.Length);
            return mergedPool[poolChar];
        }
        else
        {
            int pool = UnityEngine.Random.Range(0, pools.Count);
            int poolChar = UnityEngine.Random.Range(0, pools[pool].Length);
            return pools[pool][poolChar];
        }
    }
    
    private string getPrefix()
    {
        return prefixList.Count > 0 ?  prefixList[UnityEngine.Random.Range(0, prefixList.Count)] : "";
    }

    private string getSuffix()
    {
        return suffixList.Count > 0 ?  suffixList[UnityEngine.Random.Range(0, suffixList.Count)] : "";
    }
}