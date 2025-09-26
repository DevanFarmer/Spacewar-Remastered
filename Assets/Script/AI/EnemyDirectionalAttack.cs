using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionalAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float minAttackSpeed;
    [SerializeField] float maxAttackSpeed;
    [SerializeField] List<Vector2> attackDirections = new();
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
        GameObject proj;
        foreach (Vector2 attackDir in attackDirections) 
        {
            proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            proj.GetComponent<ProjectileComponent>().SetDirection(attackDir);
        }
    }
}
