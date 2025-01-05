using System;

using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Manage actor movement through rigidbody
/// </summary>
public abstract class MoveHandler : CoreComponent, IMoveHandler {
    protected abstract Rigidbody2D RB { get; }

    protected abstract NavMeshAgent Agent { get; }

    protected abstract BindableProperty<float> MoveSpeed { get; }

    public float MaxCooldownTime 
        => m_StateData_Dash.Cooldown;

    public float CurCooldownTime 
        => m_StateData_Dash.curCooldown;

    public bool IsReadyToDash 
        => m_StateData_Dash.curCooldown <= 0;

    [field: SerializeField] public MoveState CurState 
        { get; private set; } = MoveState.Nope;

    [SerializeField] private StateData_Dash m_StateData_Dash = new() { 
        Speed = 20, 
        Distance = 2.5f,
        Cooldown = 5,
    };

    public enum MoveState {
        Nope,
        MoveByDir,
        MoveByPos,
        Dash,
    }

    [Serializable] private struct StateData_Dash {
        public float Speed;
        public float Distance;
        public float Cooldown;
        [HideInInspector] public float curCooldown;
    }

    private void UpdateState_Nope() 
        { }

    private void UpdateState_MoveByDir() {
        if (RB.velocity.magnitude != 0)
            RB.velocity *= MoveSpeed.Value / RB.velocity.magnitude;
    }

    private void UdpateState_MoveByPos() {
        if (Agent.hasPath == false) StopMove();
        Agent.speed = MoveSpeed.Value;
    }

    private void UpdateState_Dash() {
        if (Agent.hasPath == false) StopMove();
    }

    private void FixedUpdate() {
        m_StateData_Dash.curCooldown = Math.Max(-1, 
            m_StateData_Dash.curCooldown - Time.fixedDeltaTime);

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

    public Vector2 Velocity 
        => RB.velocity != Vector2.zero 
        ? RB.velocity 
        : Agent.velocity;

    public virtual void MoveByDir(Vector2 dir) {
        CurState = MoveState.MoveByDir;
        RB.velocity = dir.normalized * MoveSpeed.Value;
    }

    public void MoveByPos(Vector2 pos) {
        CurState = MoveState.MoveByPos;
        Agent.speed = MoveSpeed.Value;
        Agent.SetDestination(pos);
    }

    public void Dash(Vector2 dir) {
        CurState = MoveState.Dash;
        m_StateData_Dash.curCooldown = m_StateData_Dash.Cooldown;
        Agent.speed = m_StateData_Dash.Speed;
        Agent.SetDestination(
            (Vector2)Agent.transform.position +
            dir.normalized * m_StateData_Dash.Distance);
    }

    public void StopMove() {
        CurState = MoveState.Nope;
        RB.velocity = Vector2.zero;
        Agent.ResetPath();
    }
}