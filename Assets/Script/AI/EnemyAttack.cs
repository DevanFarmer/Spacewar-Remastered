using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float minAttackSpeed;
    [SerializeField] float maxAttackSpeed;
    float attackSpeed;

    float lastAttackTime;

    private void Start()
    {
        SetAttackSpeed();
    }

    void Update()
    {
        if (lastAttackTime + attackSpeed <= Time.time)
        {
            Attack();
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
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }
}
