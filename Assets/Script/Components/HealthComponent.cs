using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    public UnityEvent onDamageTaken;
    public UnityEvent onHealthChange;
    public UnityEvent onDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        onHealthChange?.Invoke();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;

        onDamageTaken?.Invoke();

        onHealthChange?.Invoke();

        if (currentHealth < 0 )
        {
            Die();
        }
    }

    void Die()
    {
        onDeath?.Invoke();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
