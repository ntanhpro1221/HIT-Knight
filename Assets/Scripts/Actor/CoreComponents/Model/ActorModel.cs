using UnityEngine;
using UnityEngine.Events;

public class ActorModel : CoreComponent, IActorModelBehaviour {
    public void AttachAtkSpeed(Observable<float> atkSpeed) {
        throw new System.NotImplementedException();
    }

    public void AttachMoveSpeed(Observable<float> moveSpeed) {
        throw new System.NotImplementedException();
    }

    public void OnAtkTouched() {
        throw new System.NotImplementedException();
    }

    public void SetState_Attack(UnityAction onAtkTouched) {
        throw new System.NotImplementedException();
    }

    public void SetState_Dead() {
        throw new System.NotImplementedException();
    }

    public void SetState_Idle() {
        throw new System.NotImplementedException();
    }

    public void SetState_Move() {
        throw new System.NotImplementedException();
    }
}
