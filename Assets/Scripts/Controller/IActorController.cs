using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allow object to register to be called when have corresponding control request
/// </summary>
public interface IActorController {
    void SetMovementListener(UnityAction<Vector2> callback);
    void SetDashListener(UnityAction callback);
    void SetAttackListener(UnityAction callback);
}
