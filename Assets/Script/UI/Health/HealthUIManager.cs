using TMPro;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    #region Singleton
    public static HealthUIManager Instance { get; private set; }

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

    [SerializeField] TextMeshProUGUI healthText;

    private void Awake()
    {
        HandleSingleton();
    }

    public void SetHealthText(TextMeshProUGUI _healthText)
    {
        healthText = _healthText;
    }

    public void UpdateHealthText(float health)
    {
        healthText.text = health.ToString();
    }
}
