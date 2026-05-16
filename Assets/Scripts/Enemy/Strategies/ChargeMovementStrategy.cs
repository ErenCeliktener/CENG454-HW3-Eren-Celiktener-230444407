using UnityEngine;

public class ChargeMovementStrategy : MonoBehaviour, IEnemyMovementStrategy
{
    [Header("Charge Settings")]
    [SerializeField] private float chargeInterval = 2.5f;
    [SerializeField] private float chargeDuration = 0.45f;
    [SerializeField] private float chargeSpeedMultiplier = 2.5f;
    [SerializeField] private float windupDuration = 0.35f;

    private float timer;
    private float chargeTimer;
    private float windupTimer;

    private bool isWindingUp;
    private bool isCharging;

    private Vector2 lockedChargeDirection;

    public Vector2 GetMoveDirection(
        Transform enemyTransform,
        Transform targetTransform,
        float deltaTime
    )
    {
        if (targetTransform == null)
        {
            return Vector2.zero;
        }

        timer += deltaTime;

        if (!isWindingUp && !isCharging && timer >= chargeInterval)
        {
            StartWindup(enemyTransform, targetTransform);
        }

        if (isWindingUp)
        {
            windupTimer -= deltaTime;

            if (windupTimer <= 0f)
            {
                StartCharge();
            }

            return Vector2.zero;
        }

        if (isCharging)
        {
            chargeTimer -= deltaTime;

            if (chargeTimer <= 0f)
            {
                StopCharge();
            }

            return lockedChargeDirection * chargeSpeedMultiplier;
        }

        Vector2 normalDirection = targetTransform.position - enemyTransform.position;
        return normalDirection.normalized;
    }

    private void StartWindup(Transform enemyTransform, Transform targetTransform)
    {
        isWindingUp = true;
        windupTimer = windupDuration;

        lockedChargeDirection = targetTransform.position - enemyTransform.position;
        lockedChargeDirection.Normalize();
    }

    private void StartCharge()
    {
        isWindingUp = false;
        isCharging = true;
        chargeTimer = chargeDuration;
        timer = 0f;
    }

    private void StopCharge()
    {
        isCharging = false;
    }
}