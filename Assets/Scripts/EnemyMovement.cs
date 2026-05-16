using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private int contactDamage = 1;

    private Rigidbody2D rb;
    private IEnemyMovementStrategy movementStrategy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementStrategy = GetComponent<IEnemyMovementStrategy>();

        if (movementStrategy == null)
        {
            Debug.LogWarning($"{name} has no enemy movement strategy.");
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    private void FixedUpdate()
    {
        if (target == null || rb == null || movementStrategy == null) return;

        Vector2 direction = movementStrategy.GetMoveDirection(
            transform,
            target,
            Time.fixedDeltaTime
        );

        rb.linearVelocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != target) return;

        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(contactDamage);
        }

        Destroy(gameObject);
    }
}