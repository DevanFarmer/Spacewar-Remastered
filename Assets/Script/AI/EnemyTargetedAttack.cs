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

        proj.GetComponent<ProjectileComponent>().SetDirection((target.position - transform.position).normalized);
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
