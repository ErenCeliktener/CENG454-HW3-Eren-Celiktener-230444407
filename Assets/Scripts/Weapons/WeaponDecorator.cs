using UnityEngine;

public abstract class WeaponDecorator : IWeapon
{
    protected readonly IWeapon wrappedWeapon;

    public virtual float FireCooldown => wrappedWeapon.FireCooldown;

    protected WeaponDecorator(IWeapon wrappedWeapon)
    {
        this.wrappedWeapon = wrappedWeapon;
    }

    public virtual void Fire(
        ProjectilePool projectilePool,
        Transform firePoint,
        Camera mainCamera
    )
    {
        wrappedWeapon.Fire(projectilePool, firePoint, mainCamera);
    }
}