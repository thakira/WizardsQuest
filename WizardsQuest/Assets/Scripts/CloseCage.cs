
using UnityEngine;

 
public class CloseCage : MonoBehaviour
{
    float smooth = 2.0f;
    float DoorOpenAngle = 60f;
    float DoorCloseAngle = 0.0f;
    public bool open = true;
    [SerializeField] private AudioClip metalDoor;
    

    void Update()
    {
        if(open)
        {
            //print("if(open)");
            var target = Quaternion.Euler (0, DoorOpenAngle, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
        }
       
        if(!open)
        {
            //print("if(!open)");
            var target1= Quaternion.Euler (0, DoorCloseAngle, 0);
            AudioSource.PlayClipAtPoint(metalDoor, transform.position);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * smooth);
        }
    }
}

