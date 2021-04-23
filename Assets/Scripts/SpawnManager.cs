using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From Brackey's tutorial on Object Pooling

public class SpawnManager : MonoBehaviour
{
    ObjectPooler objectPooler;
    // add a public list with tags of things to spawn?

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        // add a loop over tags of things to spawn?
        objectPooler.SpawnFromPool("ThingToSpawn", transform.position, Quaternion.identity);
    }
}