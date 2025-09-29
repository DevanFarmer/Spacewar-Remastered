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
    [SerializeField] GameObject bossPrefab;
    [SerializeField] float bossTimeToSpawn;
    [SerializeField] float bossSpawnHeightPadding;
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

    Vector2 GetBossSpawnPos()
    {
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - Camera.main.transform.position.z));
        return new Vector3(0f, topRight.y + bossSpawnHeightPadding, 0f);
    }

    // Will have a scriptable object to handle boss spawning and setup
    void HandleBossSpawning()
    {
        if (timeSinceStart < bossTimeToSpawn) return;

        GameObject boss = Instantiate(bossPrefab, GetBossSpawnPos(), Quaternion.identity);
        // will have a interface to handle different types

        boss.GetComponentInChildren<EnemyTargetedAttack>().SetTarget(player.transform);

        boss.GetComponent<Boss_1_Movement>().onEntered.AddListener(() => EnemySpawner.Instance.SetSpawnState(true));

        boss.GetComponent<HealthComponent>().onDeath.AddListener(() => EnemySpawner.Instance.SetSpawnState(false));

        boss.GetComponent<HealthComponent>().onDeath.AddListener(() =>
        boss.GetComponent<BossMovementUtilities>().MoveToDeathLocation(
            new Vector2(0, CameraUtilities.Instance.GetTop() - boss.GetComponent<SpriteRenderer>().bounds.extents.y),
            1f));

        bossSpawned = true;
    }

    public Transform GetPlayer()
    {
        return player.transform;
    }
}
