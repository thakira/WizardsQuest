/*using System;
using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;
/*using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = System.Random;*/

public class AxeImpact : MonoBehaviour

{
    [SerializeField] private AudioClip impactSound;
    private AudioSource impact;
    private float velToVol = .2f;

    private void Awake()
    {
        impact = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Axt")
        {
            float hitVol = other.relativeVelocity.magnitude * velToVol;
            impact.PlayOneShot(impactSound,1f);
        }
    }
}
