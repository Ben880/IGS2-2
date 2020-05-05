using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLController : MonoBehaviour
{
    [SerializeField]
    private string lookForTag;
    private bool checkedForOther = false;
    [SerializeField]
    private bool awakeState = false;
    void Awake()
    {
        switchChildren(awakeState);
    }

    private void switchChildren(bool b)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(b);
        }
    }
    // Start is called before the first frame update
    private void LateUpdate()
    {
        if (!checkedForOther)
        {
            GameObject other = GameObject.FindGameObjectWithTag(lookForTag);
            if (other == null)
            {
                switchChildren(true);
            }
            else
            {
                other.transform.position = transform.GetChild(0).position;
            }
            checkedForOther = true;
        }
    }
}
