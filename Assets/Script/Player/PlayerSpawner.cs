using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    #region Singleton
    public static PlayerSpawner Instance { get; private set; }

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

    [SerializeField] GameObject Player_Prefab;
    [SerializeField] float yPercentSpawn;

    Camera cam;
    float camHeight, yBottom, yTop;

    private void Awake()
    {
        HandleSingleton();

        cam = Camera.main;

        camHeight = cam.orthographicSize;
        yTop = cam.transform.position.y - camHeight;
        yBottom = cam.transform.position.y + camHeight;
    }

    [ContextMenu("Spawn Player")]
    public GameObject SpawnPlayer()
    {
        GameObject player = Instantiate(Player_Prefab, new Vector3(0, Mathf.Lerp(yTop, yBottom, yPercentSpawn), 0), Quaternion.identity);

        SetupHealthUI(player);

        return player;
    }

    void SetupHealthUI(GameObject player)
    {
        HealthComponent health = player.GetComponent<HealthComponent>();
        health.onHealthChange.AddListener(() => HealthUIManager.Instance.UpdateHealthText(health.GetCurrentHealth()));
    }
}
