using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance { get; private set; }

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

    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Boss")]
    [SerializeField] BaseBossScriptableObject boss;
    [SerializeField] float bossTimeToSpawn; // will be set in BossSO
    [SerializeField] float bossSpawnHeightPadding; // will be set in BossSO
    bool bossSpawned;
    float timeSinceStart;

    private void Awake()
    {
        HandleSingleton();
    }

    void Start()
    {
        player = PlayerSpawner.Instance.SpawnPlayer();
        
        bossSpawned = false;

        timeSinceStart = 0f;

        EnemySpawner.Instance.SetSpawnState(true);
    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime;

        if (!bossSpawned && EnemySpawner.Instance.EnemiesSpawning && timeSinceStart >= bossTimeToSpawn * 0.75f) EnemySpawner.Instance.SetSpawnState(false);

        if (!bossSpawned) HandleBossSpawning();
    }

    void HandleBossSpawning()
    {
        if (timeSinceStart < bossTimeToSpawn) return;

        boss.SpawnBoss();

        bossSpawned = true;
    }

    public Transform GetPlayer()
    {
        return player.transform;
    }
}
