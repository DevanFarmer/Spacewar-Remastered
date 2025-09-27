using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    [Header("Strafe")]
    [SerializeField] bool strafes;
    [SerializeField] float strafeSpeed;
    [SerializeField] bool strafeChangeWithTime;
    [SerializeField] float minStrafeChangeTime;
    [SerializeField] float maxStrafeChangeTime;
    float strafeChangeTime;
    float lastStrafeChangeTime;

    [Header("Direction")]
    [SerializeField] Vector2 movement;
    // really not sure what causes movement.x to be not 0 after being spawned, even when prefab is 0, not consistant
    // possibly just that they spawned on the edge

    [Header("Offscreen Cleanup")]
    [SerializeField] float destroyDelayDistance;

    Camera cam;
    Rigidbody2D rb;
    float halfWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;

        strafeChangeTime = Random.Range(minStrafeChangeTime, maxStrafeChangeTime);

        lastStrafeChangeTime = Time.time;
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
        // ignore if no strafe
        if (!strafes) return;

        if (lastStrafeChangeTime + strafeChangeTime <= Time.time && strafeChangeWithTime)
        {
            movement.x *= -1;
            lastStrafeChangeTime = Time.time;
        }

        if (transform.position.x >= topRight.x - halfWidth)
        {
            movement.x = -1;
            lastStrafeChangeTime = Time.time; // so strafe change does immediatly happen after bouncing off edge 
        }
        if (transform.position.x <= bottomLeft.x + halfWidth)
        {
            movement.x = 1;
            lastStrafeChangeTime = Time.time;
        }
    }

    void HandleMovement()
    {
        rb.MovePosition(rb.position + new Vector2(movement.x * strafeSpeed, movement.y * moveSpeed) * Time.fixedDeltaTime);
    }
}
