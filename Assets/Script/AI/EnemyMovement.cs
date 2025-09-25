using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [SerializeField] float strafeSpeed;

    [SerializeField] Vector2 movement;

    [SerializeField] float destroyDelayDistance;

    Camera cam;
    Rigidbody2D rb;
    float halfWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    Vector3 bottomLeft;
    Vector3 topRight;
    void Update()
    {
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - cam.transform.position.z));
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - cam.transform.position.z));

        if (transform.position.y <= bottomLeft.y - destroyDelayDistance)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        HandleStrafe();
        HandleMovement();
    }

    void HandleStrafe()
    {
        if (transform.position.x >= topRight.x - halfWidth)
        {
            movement.x = -1;
        }
        if (transform.position.x <= bottomLeft.x + halfWidth)
        {
            movement.x = 1;
        }
    }

    void HandleMovement()
    {
        rb.MovePosition(rb.position + new Vector2(movement.x * strafeSpeed, movement.y * moveSpeed) * Time.fixedDeltaTime);
    }
}
