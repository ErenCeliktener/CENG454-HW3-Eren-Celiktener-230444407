using UnityEngine;

public class BaseProjectileWeapon : IWeapon
{
    private readonly float fireCooldown;

    public float FireCooldown => fireCooldown;

    public BaseProjectileWeapon(float fireCooldown)
    {
        this.fireCooldown = fireCooldown;
    }

    public void Fire(
        ProjectilePool projectilePool,
        Transform firePoint,
        Camera mainCamera
    )
    {
        if (projectilePool == null || firePoint == null || mainCamera == null)
        {
            return;
        }

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        Vector2 direction = mouseWorldPosition - firePoint.position;

        if (direction.sqrMagnitude <= 0.001f)
        {
            return;
        }

        projectilePool.Get(
            firePoint.position,
            Quaternion.identity,
            direction.normalized
        );
    }
}