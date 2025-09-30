using EventBusEventData;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager Instance { get; private set; }

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

    [SerializeField] int score;
    [SerializeField] TextMeshProUGUI scoreText;

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

    private void Start()
    {
        score = 0;
        UpdateScoreUI();
    }

    public void GainScore(int _score)
    {
        score += _score;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString();
    }

    void OnBossDefeatedEvent(OnBossDefeated e)
    {
        GainScore(e.scoreGain);

        int livesLeft = (int)GameManager.Instance.GetPlayer().GetComponent<HealthComponent>().GetCurrentHealth();

        if (livesLeft <= 0) livesLeft = 1; // just in case the player dies just before calculation

        GainScore((score * livesLeft) - score); // gain adds not sets, so subtract current score from calculated score
    }
}
