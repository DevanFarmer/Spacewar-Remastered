using EventBusEventData;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBossScriptableObject : ScriptableObject
{
    public GameObject bossPrefab;

    public float health;

    [Header("Spawning")]
    public float bossSpawnHeightPadding;
    protected float bossStartTime;
    protected float bossDefeatedTime;

    [Header("Score")]
    public List<ScoreGainByTime> scoreTimes = new();

    protected Vector2 GetBossSpawnPos()
    {
        return new Vector3(
            (CameraUtilities.Instance.GetLeft() + CameraUtilities.Instance.GetRight()) /2f, 
            CameraUtilities.Instance.GetTop() + bossSpawnHeightPadding, 
            0f);
    }

    public virtual void SpawnBoss() 
    { 
        EventBus.Subscribe<OnBossEntered>(HandleOnBossEntered);
    }

    float timeTakenToDefeat;
    protected int GetTotalScoreGain()
    {
        timeTakenToDefeat = bossDefeatedTime - bossStartTime;

        foreach (ScoreGainByTime scoreGainByTime in scoreTimes)
        {
            if (timeTakenToDefeat < scoreGainByTime.time) return scoreGainByTime.scoreGain;
        }

        // return last by default
        return scoreTimes[scoreTimes.Count - 1].scoreGain;
    }

    void HandleOnBossEntered(OnBossEntered e)
    {
        bossStartTime = Time.time;

        EventBus.Unsubscribe<OnBossEntered>(HandleOnBossEntered); // since boss won't enter more than once can unsubscribe once entered
    }
}

[System.Serializable]
public class ScoreGainByTime
{
    public int scoreGain;
    public float time;
}
