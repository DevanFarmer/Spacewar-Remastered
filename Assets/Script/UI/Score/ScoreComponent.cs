using UnityEngine;

public class ScoreComponent : MonoBehaviour
{
    [SerializeField] int scoreGain;
    ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;
    }

    public void SetScoreGain(int _score)
    {
        scoreGain = _score;
    }

    public void GainScore()
    {
        scoreManager.GainScore(scoreGain);
    }
}
