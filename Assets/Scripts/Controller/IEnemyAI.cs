using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Automaticaly controller
/// </summary>
public abstract class IEnemyAI : CoreComponent, IActorController {
    public abstract void SetAttackListener(UnityAction callback);
    public abstract void SetDashListener(UnityAction callback);
    public abstract void SetMovementListener(UnityAction<Vector2> callback);
}
