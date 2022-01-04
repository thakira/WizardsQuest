using System;
using UnityEngine;
using System.Collections;
 
public class OpenDoor : MonoBehaviour
{
    float smooth = .5f;
    float DoorOpenAngle = -110f;
    float DoorCloseAngle = 0f;
    public bool open;
    

    void Update()
    {
        if(open)
        {
            //print("if(open)");
            var target = Quaternion.Euler (270, DoorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
        }
       
        if(open == false)
        {
            //print("if(!open)");
            var target1= Quaternion.Euler (270, DoorCloseAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * smooth);
        }
    }
}

