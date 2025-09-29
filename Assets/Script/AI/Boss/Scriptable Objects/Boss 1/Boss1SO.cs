using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss1", menuName = "Boss/Boss 1")]
public class Boss1SO : BaseBossScriptableObject
{


    public override void SpawnBoss()
    {
        base.SpawnBoss();

        GameObject boss = Instantiate(bossPrefab, GetBossSpawnPos(), Quaternion.identity);
        // will have a interface to handle different types

        boss.GetComponentInChildren<EnemyTargetedAttack>().SetTarget(GameManager.Instance.GetPlayer());

        boss.GetComponent<Boss_1_Movement>().onEntered.AddListener(() => EnemySpawner.Instance.SetSpawnState(true));

        boss.GetComponent<HealthComponent>().onDeath.AddListener(() => EnemySpawner.Instance.SetSpawnState(false));

        EventBus.Subscribe<OnBossDeathLocationReached>(HandleOnBossDeathLocationReached);
    }

    void HandleOnBossDeathLocationReached(OnBossDeathLocationReached e)
    {
        Debug.Log("Death Location Reached!");

        EventBus.Unsubscribe<OnBossDeathLocationReached>(HandleOnBossDeathLocationReached);
    }
}
