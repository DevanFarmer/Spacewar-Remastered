using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject primary_projectilePrefab;
    [SerializeField] float attackSpeed;

    float lastAttackTime;

    void Start()
    {
        lastAttackTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (lastAttackTime + attackSpeed <= Time.time)
            {
                Shoot();
                lastAttackTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        Instantiate(primary_projectilePrefab, transform.position, Quaternion.identity);
    }
}
