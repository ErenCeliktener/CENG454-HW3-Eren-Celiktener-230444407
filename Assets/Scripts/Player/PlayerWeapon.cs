using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Weapon Setup")]
    [SerializeField] private ProjectilePool projectilePool;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;

    [Header("Base Weapon")]
    [SerializeField] private float baseFireCooldown = 0.25f;

    [Header("Decorator")]
    [SerializeField] private bool useRapidFireDecorator = true;
    [SerializeField] private float rapidFireMultiplier = 0.5f;

    private IWeapon weapon;
    private float fireTimer;

    private void Awake()
    {
        BuildWeapon();
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            weapon.Fire(projectilePool, firePoint, mainCamera);
            fireTimer = weapon.FireCooldown;
        }
    }

    private void BuildWeapon()
    {
        weapon = new BaseProjectileWeapon(baseFireCooldown);

        if (useRapidFireDecorator)
        {
            weapon = new RapidFireWeaponDecorator(
                weapon,
                rapidFireMultiplier
            );
        }
    }
}