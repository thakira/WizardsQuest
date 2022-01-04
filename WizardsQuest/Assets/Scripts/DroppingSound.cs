using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingSound : MonoBehaviour
{
    [SerializeField] private BoxCollider floor;
    [SerializeField] private AudioClip dropSound;

    
    public void OnCollisionEnter(Collision other)
    {
        if (other.collider == floor)
        {
            if(dropSound != null) {
            AudioSource.PlayClipAtPoint(dropSound, floor.transform.position);
            }
        }
    }
}
