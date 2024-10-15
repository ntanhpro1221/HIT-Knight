using System;

using UnityEngine;

/// <summary>
/// Manage actor movement through rigidbody
/// </summary>
public class MoveHandler : CoreComponent, IMoveHandler {
    private Rigidbody2D m_Rb;
    private BindableProperty<float> m_MoveSpeed;

    private MoveState m_CurState = MoveState.Nope;
    private StateData_MoveByPos m_StateData_MoveByPos;
    [SerializeField] private StateData_Dash m_StateData_Dash = new() { dashSpeed = 10, dashTime = 1};

    private enum MoveState {
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
        public float dashTime;
        [HideInInspector] public float curDashTime;
    }

    private void UpdateState_Nope() { }
    private void UpdateState_MoveByDir() { }
    private void UdpateState_MoveByPos() {
        float disToDes = Vector2.Distance(m_Rb.position, m_StateData_MoveByPos.des);
        if (disToDes <= float.Epsilon) {
            StopMove();
            return;
        }
        if (disToDes <= m_MoveSpeed.Value * Time.fixedDeltaTime) {
            m_Rb.velocity = Vector2.zero;
            m_Rb.MovePosition(m_StateData_MoveByPos.des);
            return;
        }
    }
    private void UpdateState_Dash() {
        m_StateData_Dash.curDashTime -= Time.fixedDeltaTime;
        if (m_StateData_Dash.curDashTime < 0) {
            StopMove();
            return;
        }
    }
    private void FixedUpdate() {
        switch (m_CurState) {
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

    public void Init(Rigidbody2D rb, BindableProperty<float> moveSpeed) {
        m_Rb = rb;
        m_MoveSpeed = moveSpeed;
    }
    public Vector2 Velocity => m_Rb.velocity;
    public virtual void MoveByDir(Vector2 dir) {
        m_CurState = MoveState.MoveByDir;
        m_Rb.velocity = dir.normalized * m_MoveSpeed.Value;
    }
    public void MoveByPos(Vector2 pos) {
        m_CurState = MoveState.MoveByPos;
        m_StateData_MoveByPos.des = pos;
        m_Rb.velocity = (pos - m_Rb.position).normalized * m_MoveSpeed.Value;
    }
    public void Dash(Vector2 dir) {
        m_CurState = MoveState.Dash;
        m_StateData_Dash.curDashTime = m_StateData_Dash.dashTime;
        m_Rb.velocity = dir.normalized * m_StateData_Dash.dashSpeed;
    }
    public void StopMove() {
        m_CurState = MoveState.Nope;
        m_Rb.velocity = Vector2.zero;
    }
}