using UnityEngine;

public abstract class BaseBossScriptableObject : ScriptableObject
{
    public GameObject bossPrefab;

    public float health;

    [Header("Spawning")]
    public float bossSpawnHeightPadding;

    protected Vector2 GetBossSpawnPos()
    {
        return new Vector3(
            (CameraUtilities.Instance.GetLeft() + CameraUtilities.Instance.GetRight()) /2f, 
            CameraUtilities.Instance.GetTop() + bossSpawnHeightPadding, 
            0f);
    }

    public virtual void SpawnBoss() { }
}
