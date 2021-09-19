using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _buyerPrefab;
    [SerializeField] private Vector3 _spawnPosition;
    [SerializeField] private float _spawnDelay = 1;
    private void Start()
    {
        StartSpawn();
    }

    public void StartSpawn()
    {
        StartCoroutine("Spawn");
    }
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_spawnDelay);
        Instantiate(_buyerPrefab, _spawnPosition, Quaternion.identity);
    }
}
