using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyer : MonoBehaviour
{
    private Spawner _spawner;

    public Spawner Spawner
    {
        set => _spawner = value;
    }

    private void Delete()
    {
        _spawner.StartSpawn();
        Destroy(gameObject);
    }
}
