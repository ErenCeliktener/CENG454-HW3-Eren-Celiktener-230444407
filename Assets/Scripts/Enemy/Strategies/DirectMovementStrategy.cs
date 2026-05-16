using UnityEngine;

public class DirectMovementStrategy : MonoBehaviour, IEnemyMovementStrategy
{
    public Vector2 GetMoveDirection(
        Transform enemyTransform,
        Transform targetTransform,
        float deltaTime
    )
    {
        Vector2 direction = targetTransform.position - enemyTransform.position;
        return direction.normalized;
    }
}