using UnityEngine;
using UnityEngine.Events;

public interface IActorController {
    void SetMovementListener(UnityAction<Vector2> callback);
    void SetDashListener(UnityAction callback);
    void SetAttackListener(UnityAction callback);
}
