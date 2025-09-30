using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] bool isInvincible;

    public UnityEvent onDamageTaken;
    public UnityEvent onHealthChange;
    public UnityEvent onDeath;

    bool isDead;

    private void Start()
    {
        currentHealth = maxHealth;
        onHealthChange?.Invoke();

        isInvincible = false;

        isDead = false;
    }

    public void TakeDamage(float _damage)
    {
        if (isInvincible) return;

        currentHealth -= _damage;

        onDamageTaken?.Invoke();
        onHealthChange?.Invoke();

        if (currentHealth <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        onDeath?.Invoke();
        isDead = true;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void ActiveInvincibility(float time)
    {
        StartCoroutine(HandleInvincibility(time));
    }

    IEnumerator HandleInvincibility(float time)
    {
        isInvincible = true;

        yield return new WaitForSeconds(time);

        isInvincible = false;
    }
}
