using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float fireCooldown = 0.25f;

    private float cooldownTimer;

    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        UpdateCooldown();
        HandleFireInput();
    }

    private void HandleFireInput()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (cooldownTimer > 0f)
            return;

        Fire();
        cooldownTimer = fireCooldown;
    }

    private void Fire()
    {
        if (projectilePool == null || firePoint == null || mainCamera == null)
            return;

        Vector2 shootDirection = GetMouseDirection();

        projectilePool.Get(
            firePoint.position,
            Quaternion.identity,
            shootDirection
        );
    }

    private Vector2 GetMouseDirection()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector2 direction = mouseWorldPosition - firePoint.position;

        if (direction.sqrMagnitude < 0.01f)
        {
            return transform.right;
        }

        return direction.normalized;
    }

    private void UpdateCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}