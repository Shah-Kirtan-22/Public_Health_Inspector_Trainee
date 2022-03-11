using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Animator Door;

    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Door.SetBool("isOpening", true);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Door.SetBool("isOpening", false);
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        
    }

    private void Start()
    {
        Door = this.transform.parent.GetComponent<Animator>();
    }
}
