using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private LayerMask damageableLayers;

    [Header("Hit Detection")]
    [SerializeField] private float hitRadius = 0.12f;

    private Vector2 direction;
    private float lifeTimer;
    private bool isActive;
    private Action<Projectile> returnToPool;

    public void Initialize(Vector2 launchDirection, Action<Projectile> returnCallback)
    {
        direction = launchDirection.normalized;
        lifeTimer = lifeTime;
        returnToPool = returnCallback;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }

        float moveDistance = speed * Time.deltaTime;
        Vector2 currentPosition = transform.position;

        RaycastHit2D hit = Physics2D.CircleCast(
            currentPosition,
            hitRadius,
            direction,
            moveDistance,
            damageableLayers
        );

        if (hit.collider != null)
        {
            HitTarget(hit.collider);
            return;
        }

        transform.position += (Vector3)(direction * moveDistance);

        lifeTimer -= Time.deltaTime;

        if (lifeTimer <= 0f)
        {
            Despawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive)
        {
            return;
        }

        if (!IsInDamageableLayer(other.gameObject))
        {
            return;
        }

        HitTarget(other);
    }

    private void HitTarget(Collider2D targetCollider)
    {
        IDamageable damageable = targetCollider.GetComponent<IDamageable>();

        if (damageable == null)
        {
            return;
        }

        damageable.TakeDamage(damage);
        Despawn();
    }

    private bool IsInDamageableLayer(GameObject target)
    {
        return (damageableLayers.value & (1 << target.layer)) != 0;
    }

    private void Despawn()
    {
        if (!isActive)
        {
            return;
        }

        isActive = false;
        returnToPool?.Invoke(this);
    }

    public void OnSpawned()
    {
        gameObject.SetActive(true);
        direction = Vector2.zero;
        lifeTimer = lifeTime;
        isActive = false;
    }

    public void OnDespawned()
    {
        isActive = false;
        direction = Vector2.zero;
        lifeTimer = 0f;
        returnToPool = null;
        gameObject.SetActive(false);
    }
}