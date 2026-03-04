using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    
    float spawnTimer;

    private void Awake()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= 0.2f)
        {
            spawnTimer = 0;
            Spawn();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.poolManager.Get(1);
        }
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.poolManager.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    }
}
