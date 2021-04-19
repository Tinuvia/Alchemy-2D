using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // --- ObjectPooler objectPooler;
    // add a public list with tags of things to spawn?
    public GameObject cubePrefab;

    private void Start()
    {
        // --- objectPooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        // add a loop over tags of things to spawn?
        // --- ObjectPooler.Instance.SpawnFromPool("ThingToSpawn", transform.position, Quaternion.identity);
        Instantiate(cubePrefab, transform.position, Quaternion.identity);
    }
}