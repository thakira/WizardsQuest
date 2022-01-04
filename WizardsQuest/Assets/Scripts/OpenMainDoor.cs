using System;
using System.Collections;
using System.Collections.Generic;
using CameraFading;
using UnityEngine;

public class OpenMainDoor : MonoBehaviour
{
    [SerializeField] private CapsuleCollider rightHand;
    [SerializeField] private CapsuleCollider leftHand;
    [SerializeField] private GameObject Urkunde;
    [SerializeField] private AudioClip doorUnlock;
    [SerializeField] private GameObject alias;
    private OpenDoor openDoor;
    [SerializeField] private GameObject mainDoor;
    [SerializeField] private Casting castingScript;
    private GameObject knauf;

    
    // Start is called before the first frame update
    void Start()
    {
        openDoor = GetComponent<OpenDoor>();
        openDoor = GameObject.Find("Tuer").GetComponent<OpenDoor>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == leftHand || other == rightHand)
        {
            if (castingScript.cageClosed)
            {
                
           
            alias.SetActive(false);
            AudioSource.PlayClipAtPoint(doorUnlock, transform.position);
            openDoor.open = true;
            StartCoroutine(EndGame(Fading));
            }
        }
    }

    IEnumerator EndGame(Action fading)
    {
        yield return new WaitForSeconds(2f);
        Urkunde.SetActive(true);
        yield return new WaitForSeconds(5f);
        fading();

    }

    private void Fading()
    {
        CameraFade.Out(5f);
        Application.Quit();
    }
}


