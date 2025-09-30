using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss1", menuName = "Boss/Boss 1")]
public class Boss1SO : BaseBossScriptableObject
{
    GameObject boss; // this and instantiating boss will be in base

    public override void SpawnBoss()
    {
        base.SpawnBoss();

        boss = Instantiate(bossPrefab, GetBossSpawnPos(), Quaternion.identity);
        // will have a interface to handle different types

        boss.GetComponentInChildren<EnemyTargetedAttack>().SetTarget(GameManager.Instance.GetPlayer());

        boss.GetComponent<Boss_1_Movement>().onEntered.AddListener(() => EnemySpawner.Instance.SetSpawnState(true));
        boss.GetComponent<Boss_1_Movement>().onEntered.AddListener(() => EventBus.Publish(new OnBossEntered()));

        boss.GetComponent<HealthComponent>().onDeath.AddListener(() => bossDefeatedTime = Time.time);
        boss.GetComponent<HealthComponent>().onDeath.AddListener(() => EnemySpawner.Instance.SetSpawnState(false));

        boss.GetComponent<HealthComponent>().onDeath.AddListener(() =>
        boss.GetComponent<BossMovementUtilities>().MoveToLocation(
            new Vector2(0, CameraUtilities.Instance.GetTop() - boss.GetComponent<SpriteRenderer>().bounds.extents.y),
            1f,
            new OnBossDeathLocationReached(1)));

        EventBus.Subscribe<OnBossDeathLocationReached>(HandleOnBossDeathLocationReached);
    }

    // will add a var to location reached event to check switch state the death is, first is pieces fly off animation, second is final death
    void HandleOnBossDeathLocationReached(OnBossDeathLocationReached e)
    {
        Debug.Log($"Death Location {e.bossDeathState} Reached!");
        
        if (e.bossDeathState == 1)
        {
            HandleFirstDeathLocation();
        }
        else if (e.bossDeathState == 2)
        {
            HandleSecondDeathLocation();
        }
    }

    void HandleFirstDeathLocation()
    {
        // play animation(pieces fly off, big explosion that wipes away all enemies)
        // after animation next move to location will be called

        boss.GetComponent<BossMovementUtilities>().MoveToLocation(
            new Vector2(0, CameraUtilities.Instance.GetTop() + 4f),
            1f,
            new OnBossDeathLocationReached(2));
    }

    void HandleSecondDeathLocation()
    {
        EventBus.Publish(new OnBossDefeated(GetTotalScoreGain()));

        EventBus.Unsubscribe<OnBossDeathLocationReached>(HandleOnBossDeathLocationReached);

        boss.SetActive(false);
    }
}
