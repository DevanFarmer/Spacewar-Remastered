using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Singleton
    public static EnemySpawner Instance { get; private set; }

    void HandleSingleton()
    {
        if (Instance != null)
        {
            DestroyImmediate(Instance);
            return;
        }

        Instance = this;
    }
    #endregion

    [SerializeField] List<GameObject> enemies = new();
    [SerializeField] bool spawnEnemies;

    [Header("Spawn Time")]
    [SerializeField] float minSpawnTime;
    [SerializeField] float maxSpawnTime;

    [Header("Spawn Pos")]
    [SerializeField] float minSpawnYOffset;
    [SerializeField] float maxSpawnYOffset;

    [Header("Spawn Amount")]
    [SerializeField] int minSpawns;
    [SerializeField] int maxSpawns;

    float spawnTime;
    float lastSpawnTime;

    Camera cam;

    private void Awake()
    {
        HandleSingleton();
    }

    void Start()
    {
        lastSpawnTime = Time.time;

        cam = Camera.main;
    }

    void Update()
    {
        if (!spawnEnemies) return;

        if (lastSpawnTime + spawnTime <= Time.time)
        {
            SpawnEnemies();
            SetSpawnTime();
            lastSpawnTime = Time.time;
        }
    }

    void SetSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    GameObject SelectEnemy()
    {
        return enemies[Random.Range(0, enemies.Count)];
    }

    float GetSpawnXPos()
    {
        return Random.Range(bottomLeft.x, topRight.x);
    }

    float GetSpawnYPos()
    {
        return topRight.y + Random.Range(minSpawnYOffset, maxSpawnYOffset);
    }

    Vector3 GetSpawnPos()
    {
        return new Vector3(GetSpawnXPos(), GetSpawnYPos(), 0);
    }

    Vector3 bottomLeft, topRight;
    void SpawnEnemies()
    {
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - cam.transform.position.z));
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - cam.transform.position.z));

        for (int i = 0; i <= Random.Range(minSpawns, maxSpawns); i++) 
        {
            Instantiate(SelectEnemy(), GetSpawnPos(), Quaternion.identity);
            // if too close to another enemy move up by sprite height
        }
    }
}
