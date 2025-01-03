using System;

using UnityEngine;

/// <summary>
/// Manage actor movement through rigidbody
/// </summary>
public abstract class MoveHandler : CoreComponent, IMoveHandler {
    protected abstract Rigidbody2D RB { get; }
    protected abstract BindableProperty<float> MoveSpeed { get; }

    public float MaxCooldownTime => m_StateData_Dash.dashCoolDownTime;
    public float CurCooldownTime => m_StateData_Dash.curDashCooldownTime;
    public bool IsReadyToDash => m_StateData_Dash.curDashCooldownTime <= 0;
    [field: SerializeField]
    public MoveState CurState { get; private set; } = MoveState.Nope;
    private StateData_MoveByPos m_StateData_MoveByPos;
    [SerializeField] private StateData_Dash m_StateData_Dash = new() { 
        dashSpeed = 20, 
        dashDuration = 0.15f,
        dashCoolDownTime = 5,
    };

    public enum MoveState {
        Nope,
        MoveByDir,
        MoveByPos,
        Dash,
    }
    private struct StateData_MoveByPos {
        public Vector2 des;
    }
    [Serializable] private struct StateData_Dash {
        public float dashSpeed;
        public float dashDuration;
        public float dashCoolDownTime;
        [HideInInspector] public float curDashDuration;
        [HideInInspector] public float curDashCooldownTime;
    }

    private void UpdateState_Nope() { }
    private void UpdateState_MoveByDir() { }
    private void UdpateState_MoveByPos() {
        float disToDes = Vector2.Distance(RB.position, m_StateData_MoveByPos.des);
        if (disToDes <= float.Epsilon) {
            StopMove();
            return;
        }
        if (disToDes <= MoveSpeed.Value * Time.fixedDeltaTime) {
            RB.velocity = Vector2.zero;
            RB.MovePosition(m_StateData_MoveByPos.des);
            return;
        }
    }
    private void UpdateState_Dash() {
        m_StateData_Dash.curDashDuration -= Time.fixedDeltaTime;
        if (m_StateData_Dash.curDashDuration < 0) {
            StopMove();
            return;
        }
    }
    private void FixedUpdate() {
        m_StateData_Dash.curDashCooldownTime = Math.Max(-1, 
            m_StateData_Dash.curDashCooldownTime - Time.fixedDeltaTime);

        switch (CurState) {
            case MoveState.Nope: UpdateState_Nope(); break;
            case MoveState.MoveByDir: UpdateState_MoveByDir(); break;
            case MoveState.MoveByPos: UdpateState_MoveByPos(); break;
            case MoveState.Dash: UpdateState_Dash(); break;
            default:
#if UNITY_EDITOR
                Debug.LogWarning(UnityEditor.Search.SearchUtils.GetHierarchyPath(gameObject) + ": Undefined current move state");
#endif
                break;
        }
    }

    public Vector2 Velocity => RB.velocity;
    public virtual void MoveByDir(Vector2 dir) {
        CurState = MoveState.MoveByDir;
        RB.velocity = dir.normalized * MoveSpeed.Value;
    }
    public void MoveByPos(Vector2 pos) {
        CurState = MoveState.MoveByPos;
        m_StateData_MoveByPos.des = pos;
        RB.velocity = (pos - RB.position).normalized * MoveSpeed.Value;
    }
    public void Dash(Vector2 dir) {
        CurState = MoveState.Dash;
        m_StateData_Dash.curDashDuration = m_StateData_Dash.dashDuration;
        m_StateData_Dash.curDashCooldownTime = m_StateData_Dash.dashCoolDownTime;
        RB.velocity = dir.normalized * m_StateData_Dash.dashSpeed;
    }
    public void StopMove() {
        CurState = MoveState.Nope;
        RB.velocity = Vector2.zero;
    }
}