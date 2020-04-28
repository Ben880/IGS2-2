using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomSwitcher : MonoBehaviour
{
    [SerializeField] 
    private bool useMaster = true;
    [SerializeField]
    private PZ_Master master;
    [SerializeField]
    private string room;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other entered");
        if (other.transform.gameObject.tag.Equals("Hand"))
        {
            Debug.Log("other is hand");
            if ((useMaster && master.allSolved()) || !useMaster)
            {
                Debug.Log("triggerd room change");
                SceneManager.LoadScene(room);
            }
            else
            {
                //GetComponent<AudioSource>().Play();
            }
        }

    }
}
