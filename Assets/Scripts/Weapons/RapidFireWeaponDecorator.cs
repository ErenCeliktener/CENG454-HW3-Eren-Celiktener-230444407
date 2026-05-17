using UnityEngine;

public class RapidFireWeaponDecorator : WeaponDecorator
{
    private readonly float cooldownMultiplier;

    public override float FireCooldown => wrappedWeapon.FireCooldown * cooldownMultiplier;

    public RapidFireWeaponDecorator(IWeapon wrappedWeapon, float cooldownMultiplier)
        : base(wrappedWeapon)
    {
        this.cooldownMultiplier = Mathf.Clamp(cooldownMultiplier, 0.1f, 1f);
    }
}