using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] string hitTag;
    [SerializeField] float damage;

    HealthComponent healthComp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != hitTag) return;

        // has health comp?
        healthComp = collision.GetComponent<HealthComponent>();
        if (healthComp)
        {
            healthComp.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
