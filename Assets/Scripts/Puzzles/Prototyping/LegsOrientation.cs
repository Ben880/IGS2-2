using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsOrientation : MonoBehaviour
{

    public GameObject cameraObj;
    public Vector3 offsetRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraObj = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 eulerRotation = new Vector3(
            0 + offsetRotation.x, 
            cameraObj.transform.eulerAngles.y+offsetRotation.y, 
            0 + offsetRotation.z
            );
        transform.rotation = Quaternion.Euler(eulerRotation);
    }
}
