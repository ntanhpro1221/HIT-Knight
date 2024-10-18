using UnityEngine.Events;

public interface IActorModelBehaviour {
    void AttachMoveSpeed(Observable<float> moveSpeed);
    void AttachAtkSpeed(Observable<float> atkSpeed);
    void SetState_Idle();
    void SetState_Move();
    void SetState_Attack(UnityAction onAtkTouched);
    void SetState_Dead();
    void OnAtkTouched();
}
