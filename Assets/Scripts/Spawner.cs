using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _secondsRespawn = 1f;

    private List<Transform> _spawnPoints = new List<Transform>();

    private void Awake()
    {
        foreach (Transform spawPoint in transform)
            _spawnPoints.Add(spawPoint);
    }

    private void Start()
    {
        StartCoroutine(SpawnObject());
    }

    private IEnumerator SpawnObject()
    {
        var timeRespawn = new WaitForSeconds(_secondsRespawn);

        for (int i = 0; i < _spawnPoints.Count; i++)
        {
            Instantiate(_prefab, _spawnPoints[i].position, Quaternion.identity);

            yield return timeRespawn;
        }
    }
}
