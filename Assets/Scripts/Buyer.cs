using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GameObject.Find("Main Camera").GetComponent<Spawner>();
    }

    private void Delete()
    {
        _spawner.StartSpawn();
        Destroy(gameObject);
    }
}
