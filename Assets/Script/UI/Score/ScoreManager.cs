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
    }
}
