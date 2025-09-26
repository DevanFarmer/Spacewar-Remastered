using UnityEngine;

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
    private void Awake()
    {
        HandleSingleton();
    }

    void Start()
    {
        player = PlayerSpawner.Instance.SpawnPlayer();
    }
}
