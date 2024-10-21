using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// User controller
/// </summary>
public class ActorController : MonoBehaviour, IActorController {
    public void SetAttackListener(UnityAction callback) {
        throw new System.NotImplementedException();
    }
    public void SetDashListener(UnityAction callback) {
        throw new System.NotImplementedException();
    }
    public void SetMovementListener(UnityAction<Vector2> callback) {
        throw new System.NotImplementedException();
    }
}
