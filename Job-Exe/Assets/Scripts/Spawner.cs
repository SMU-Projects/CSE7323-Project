using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn = null;

    public GameObject Spawn()
    {
        return objectToSpawn;
    }
}
