using System.Collections.Generic;
using UnityEngine;

// create controller for which attacks to use and attackSpeeds
public class EnemyDirectionalAttack : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [Header("Attack Speed")]
    [SerializeField] float minAttackSpeed;
    [SerializeField] float maxAttackSpeed;
    float attackSpeed;

    float lastAttackTime;

    [Header("Attack Settings")]
    [SerializeField] Vector2 attackDirection;
    [SerializeField] int numberOfAttacks;
    [SerializeField] float distanceBetweenAttacks;
    [SerializeField] float angleBetweenAttacks;

    private void Start()
    {
        SetAttackSpeed();
    }

    void Update()
    {
        if (lastAttackTime + attackSpeed <= Time.time)
        {
            Attack(angleBetweenAttacks, numberOfAttacks, transform);
            SetAttackSpeed();
            lastAttackTime = Time.time;
        }
    }

    void SetAttackSpeed()
    {
        attackSpeed = Random.Range(minAttackSpeed, maxAttackSpeed);
    }

    GameObject SpawnProjectile(GameObject _projectile, Vector3 _position, Quaternion _rotation)
    {
        GameObject spawned_Projectile = Instantiate(_projectile, _position, _rotation);

        return spawned_Projectile;
    }

    void Attack(float _angleBetweenShots, int _numberOfShots, Transform _firePoint)
    {
        Quaternion direction = Quaternion.identity;
        if (_numberOfShots % 2 != 0) direction = Quaternion.Euler(0f, 0f, (_angleBetweenShots * (_numberOfShots / 2)) * -1f) * _firePoint.rotation;
        else direction = Quaternion.Euler(0f, 0f, ((_angleBetweenShots * (_numberOfShots / 2)) * -1f) + (_angleBetweenShots / 2)) * _firePoint.rotation;

        for (int i = 0; i < _numberOfShots; i++)
        {
            SpawnProjectile(projectilePrefab, _firePoint.position, direction);
            direction = Quaternion.Euler(0f, 0f, _angleBetweenShots) * direction;
        }
    }
}
