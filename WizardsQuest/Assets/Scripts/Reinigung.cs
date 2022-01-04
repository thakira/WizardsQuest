
/*using System;
using System.Collections;*/
using UnityEngine;

public class Reinigung : MonoBehaviour
{
    [SerializeField] private GameObject leftover;
    [SerializeField] private GameObject bubbles;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject cleancast;
    [SerializeField] private AudioSource bubbling;
    [SerializeField] private AudioClip pouring;
    private AudioSource pour;

private GameObject player;
    private Spells spellScript;

    private void Awake()
    {
        pour = GetComponent<AudioSource>();
    }

    void Start()
{
    player = GameObject.FindWithTag("Player");
    spellScript = player.GetComponent<Spells>();
}
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other == leftover) {
            pour.PlayOneShot(pouring, 1f);
            cleancast.SetActive(true);
            bubbles.SetActive(false);
            smoke.SetActive(false);
            leftover.SetActive(false);
            bubbling.Stop();
            spellScript.schwebeZauber = true;
        }
    }
}
