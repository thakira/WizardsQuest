
using System;
using UnityEngine;
using VRTK.Prefabs.Interactions.Interactables;
using System.Collections;


public class GrabChecker : MonoBehaviour
{
    public string controlledBy = "";
    private InteractableFacade interact;
    
    private void Awake()
    {
        interact = GetComponent<InteractableFacade>();
    }
    


    public void Grabbed()
    {
        Debug.Log("Grabbed");
        controlledBy = (interact.GrabbingInteractors[0]).name;
    }
    public void Ungrabbed()
    {
        Debug.Log("Ungrabbed");
        controlledBy = "";
    }
}