using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (target == null || rb == null) return;

        Vector2 direction = ((Vector2)target.position - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            Destroy(gameObject);
        }
    }
}