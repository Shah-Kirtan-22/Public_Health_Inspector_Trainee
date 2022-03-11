using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController1 : MonoBehaviour
{
    Animator mydoor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            mydoor.SetBool("isOpening", true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            mydoor.SetBool("isOpening", false);
    }

    private void Start()
    {
        mydoor = this.transform.parent.GetComponent<Animator>();
    }
}
