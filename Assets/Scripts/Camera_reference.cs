using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_reference : MonoBehaviour
{
    public GameObject player; // reference to the sphere

    private Vector3 offset; // difference between the value of camera and player

    private void Start()
    {
        // set the offset which is a fix value (diff in position of camera and player)
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = player.transform.position + offset;

    }
}
