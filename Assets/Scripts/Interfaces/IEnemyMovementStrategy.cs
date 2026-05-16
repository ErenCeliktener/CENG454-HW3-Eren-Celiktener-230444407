using UnityEngine;

public interface IEnemyMovementStrategy
{
    Vector2 GetMoveDirection(
        Transform enemyTransform,
        Transform targetTransform,
        float deltaTime
    );
}