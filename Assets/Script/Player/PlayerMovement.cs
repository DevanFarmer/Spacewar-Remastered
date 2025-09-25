using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody2D rb;
    Camera cam;

    float halfWidth, halfHeight; // for clamp padding to keep sprite in view always

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        halfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        halfHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    Vector2 input, newPos;
    void FixedUpdate()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        newPos = rb.position + input * moveSpeed * Time.fixedDeltaTime;

        newPos = ClampToCameraView(newPos);

        rb.MovePosition(newPos);
    }

    float clampedX, clampedY;
    Vector2 ClampToCameraView(Vector2 targetPos)
    {
        // Get camera boundaries in world space
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - cam.transform.position.z));
        Vector3 topRight   = cam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - cam.transform.position.z));

        clampedX = Mathf.Clamp(targetPos.x, bottomLeft.x + halfWidth, topRight.x - halfWidth);
        clampedY = Mathf.Clamp(targetPos.y, bottomLeft.y + halfHeight, topRight.y - halfHeight);

        return new Vector2(clampedX, clampedY);
    }
}
