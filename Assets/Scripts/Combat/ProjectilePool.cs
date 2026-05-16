using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private int initialSize = 20;

    private readonly Queue<Projectile> availableProjectiles = new Queue<Projectile>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
        {
            Projectile projectile = CreateProjectile();
            Return(projectile);
        }
    }

    private Projectile CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform);
        projectile.OnDespawned();
        return projectile;
    }

    public Projectile Get(Vector3 position, Quaternion rotation, Vector2 direction)
    {
        Projectile projectile = availableProjectiles.Count > 0
            ? availableProjectiles.Dequeue()
            : CreateProjectile();

        projectile.transform.SetPositionAndRotation(position, rotation);
        projectile.OnSpawned();
        projectile.Initialize(direction, Return);

        return projectile;
    }

    private void Return(Projectile projectile)
    {
        projectile.OnDespawned();
        availableProjectiles.Enqueue(projectile);
    }
}