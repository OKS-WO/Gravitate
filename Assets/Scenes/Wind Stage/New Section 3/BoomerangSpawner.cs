// BoomerangSpawner.cs
using UnityEngine;

public class BoomerangSpawner : MonoBehaviour
{
    public GameObject boomerangPrefab;
    public float spawnInterval = 3.0f; // 3초마다 발사

    void Start()
    {
        InvokeRepeating("SpawnBoomerang", 0f, spawnInterval);
    }

    void SpawnBoomerang()
    {
        Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
    }
}