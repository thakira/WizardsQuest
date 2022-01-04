/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{


    private GameObject castray;
    // Start is called before the first frame update
    void Start()
    {
        castray = GameObject.FindWithTag("castray");
        XRSettings.eyeTextureResolutionScale = 2.0f;


    }

    // Update is called once per frame
    void Update()
    {

        }
        
        
    
}
