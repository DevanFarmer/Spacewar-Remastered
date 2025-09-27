using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] string hitTag;
    [SerializeField] float damage;
    [SerializeField] bool dontDestroyOnHit; // saying dont since most will be projectiles and don't want to check if each is true

    HealthComponent healthComp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != hitTag) return;

        // has health comp?
        healthComp = collision.GetComponent<HealthComponent>();
        if (healthComp)
        {
            healthComp.TakeDamage(damage);
            if (!dontDestroyOnHit) Destroy(gameObject);
        }
    }
}
