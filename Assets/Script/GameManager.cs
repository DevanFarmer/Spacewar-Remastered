using UnityEngine;
using System.Collections.Generic;
using EventBusEventData;

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
    bool bossDefeated; // might need in future

    float timeSinceStart;

    private void OnEnable()
    {
        EventBus.Subscribe<OnBossDefeated>(OnBossDefeatedEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<OnBossDefeated>(OnBossDefeatedEvent);
    }

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

    void OnBossDefeatedEvent(OnBossDefeated e)
    {
        bossDefeated = true;
        // add score
        // next level etc.
    }
}
