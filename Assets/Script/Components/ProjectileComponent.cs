using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (Vector2)transform.up * speed * Time.fixedDeltaTime);
    }
}
