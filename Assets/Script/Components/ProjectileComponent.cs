using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }
}
