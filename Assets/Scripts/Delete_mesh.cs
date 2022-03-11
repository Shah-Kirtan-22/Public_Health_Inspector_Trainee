using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_mesh : MonoBehaviour
{
    MeshCollider[] colliders;
    private void Awake()
    {
        colliders = FindObjectsOfType<MeshCollider>();
        //Debug.Log("The total number of mesh colliders before:" + colliders.Length);
        for(int i = 0; i < colliders.Length; i++)
        {
            Destroy(colliders[i]);
        }
        
    }
}
