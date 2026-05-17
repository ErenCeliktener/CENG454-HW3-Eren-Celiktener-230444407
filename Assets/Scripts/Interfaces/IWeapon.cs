using UnityEngine;

public interface IWeapon
{
    float FireCooldown { get; }

    void Fire(
        ProjectilePool projectilePool,
        Transform firePoint,
        Camera mainCamera
    );
}