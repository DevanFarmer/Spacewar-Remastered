using UnityEngine;
using UnityEngine.Events;

// if remove strafe, this could just to a script to handle onSpawn and onEnter events
public class Boss_1_Movement : MonoBehaviour
{
    [SerializeField] float enterSpeed;

    [SerializeField] float strafeSpeed;
    int strafeDirection;

    Camera cam;
    Rigidbody2D rb;
    float halfWidth, halfHeight;

    bool hasEntered;

    public UnityEvent onSpawn;
    public UnityEvent onEntered;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        strafeDirection = 1;

        onSpawn?.Invoke();
    }

    Vector3 bottomLeft;
    Vector3 topRight;
    private void Update()
    {
        bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - cam.transform.position.z));
        topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - cam.transform.position.z));
    }

    private void FixedUpdate()
    {
        if (!hasEntered)
        {
            HandleEntering();
            return;
        }

        HandleDirection();
        HandleStrafing();
    }

    void HandleEntering()
    {
        rb.MovePosition(rb.position + Vector2.down * enterSpeed * Time.fixedDeltaTime);

        if (rb.position.y + halfHeight <= topRight.y)
        {
            hasEntered = true;
            onEntered?.Invoke();
        }
    }

    void HandleDirection()
    {
        if (transform.position.x >= topRight.x - halfWidth)
        {
            strafeDirection = -1;
        }
        if (transform.position.x <= bottomLeft.x + halfWidth)
        {
            strafeDirection = 1;
        }
    }

    void HandleStrafing()
    {
        rb.MovePosition(rb.position + (Vector2.right * strafeDirection) * strafeSpeed * Time.fixedDeltaTime);
    }
}
