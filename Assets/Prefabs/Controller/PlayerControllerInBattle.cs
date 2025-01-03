using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerControllerInBattle : MonoBehaviour {
    private PlayerInput m_Input;

    private IActor m_Player;

    [SerializeField] private GameObject m_DirectionerObj;
    private DirectionVisualizer m_Directioner;

    private Discard m_DiscardBtn;
    private Dash_CooldownUIHandler m_DashCooldownUI;
    private Attack_CooldownUIHandler m_AttackCooldownUI;

    private void Awake() {
        m_Input ??= new();        
        m_DiscardBtn = GetComponentInChildren<Discard>();
        m_DashCooldownUI = GetComponentInChildren<Dash_CooldownUIHandler>();
        m_AttackCooldownUI = GetComponentInChildren<Attack_CooldownUIHandler>();
    }

    private void OnEnable() {
        m_Input.Enable();
    }

    private void OnDisable() {
        m_Input.Disable();
    }

    public bool IsDiscard => m_DiscardBtn.StoredHoverStatus;

    public Vector2 CurDir => m_Directioner.CurDir;

    public void Init(GameObject player) {
        m_Input ??= new();

        m_Player = player.GetComponent<IActor>();

        m_Directioner = Instantiate(m_DirectionerObj).GetComponent<DirectionVisualizer>();
        m_Directioner.Init(player.transform, m_DiscardBtn);
        m_Directioner.AddInput(m_Input.InBattle.Dash);

        m_DiscardBtn.AddInput(m_Input.InBattle.Dash);

        m_DashCooldownUI.Init(() => 
            (m_Player.MoveHandler.CurCooldownTime, m_Player.MoveHandler.MaxCooldownTime));

        m_AttackCooldownUI.Init(() => 
            (m_Player.WeaponHandler.CurCoolDown, m_Player.WeaponHandler.MaxCoolDown));
    }
}
