using UnityEngine;

public class EnemyTargetedAttack : MonoBehaviour
{
    [SerializeField] bool attackEnabled;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float minAttackSpeed;
    [SerializeField] float maxAttackSpeed;
    [SerializeField] Transform target;

    float attackSpeed;

    float lastAttackTime;

    private void Start()
    {
        SetAttackSpeed();
    }

    void Update()
    {
        if (target == null) return;

        if (lastAttackTime + attackSpeed <= Time.time)
        {
            if (attackEnabled) Attack();
            SetAttackSpeed();
            lastAttackTime = Time.time;
        }
    }

    void SetAttackSpeed()
    {
        attackSpeed = Random.Range(minAttackSpeed, maxAttackSpeed);
    }

    void Attack()
    {
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector2 direction = target.position - transform.position;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        proj.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public void SetAttackEnabled(bool enabled)
    {
        attackEnabled = enabled;
    }
}
