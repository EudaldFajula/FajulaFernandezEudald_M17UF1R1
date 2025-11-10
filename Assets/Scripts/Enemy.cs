using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rayDistance = 0.5f;
    [SerializeField] private LayerMask obstacleLayer;

    private bool movingRight = false;
    private bool isFacingRight = false;

    void Update()
    {
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        FlipSprite(direction);

        Vector2 rayOrigin = transform.position;
        Vector2 rayDirection = movingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, obstacleLayer);
        Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

        if (hit.collider != null)
        {
            movingRight = !movingRight;
        }
    }

    public void FlipSprite(float horizontalInput)
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
