using UnityEngine;

/// <summary>
/// You must call Init() to use it
/// </summary>
public abstract class IControllerAI : IController {
    // dont declare or use IActor directly, just use neccessary its component
    protected bool isInited = false;
    protected Transform actorTrans;
    protected IStalker stalker;
    protected IWeaponHandler weaponHandler; 

    /// <summary>
    /// You must initialize this controller to use it
    /// </summary>
    /// <param name="actor">actor to controll</param>
    public virtual void Init(IActor actor) {
        isInited = true;
        actorTrans = actor.transform;
        stalker = actor.Stalker;
        weaponHandler = actor.WeaponHandler;
    }
}

public class LittleSoldierAI : IControllerAI
{
    [SerializeField]
    [Tooltip("Thời gian chờ trong trạng thái idle")]
    private float idleDuration = 2f; // thời gian chowff tromg trạng thái idle
    [SerializeField]
    [Tooltip("Bán kính di chuyển ngẫu nhiên khi idle")]
    private const float wanderRadius = 3f; // bán kính di chuyển ngẫu nhiên khi idle

    private float idleTimer = 0f; //đếm thời gian trong trạng thái idle
    private Vector2 wanderTarget; // vị trí di chuyển ngẫu nhiên

    private void Update()
    {
        if (isInited == false)
        {
            Util.LogWarning_WithAddress(gameObject, "LittleSoldierAI has not been initialized yet");
            return;
        }

        // lấy mục tiêu top từ stalker
        GameObject target = stalker.TopTarget.Value;

        if (target == null)
        {
            HandleIdle(); 
        }
        else
        {
            HandleTargetInteraction(target); 
        }
    }

    private void HandleIdle()
    {
        idleTimer += Time.deltaTime; 

        if (idleTimer >= idleDuration)
        {
            idleTimer = 0f; 
            wanderTarget = GetRandomWanderTarget(); //chon vị trí ngẫu nhiên
            PostControlCommand(ControlType.MoveByPos, wanderTarget);// điều khiển lính di chuyển tới vị trí mới
        }
        else
        {
            PostControlCommand(ControlType.Idle);// vẫn idel nếu chưa đủ time chờ
        }
    }
    
    /// <summary>
    /// FIXME: logic chưa chuẩn lắm
    /// </summary>
    private bool IsGoodToAttack(GameObject target) {
        bool isGood = false;

        // tính khoảng cách giữa soldier và mục tiêu
        float distance = Vector3.Distance(target.transform.position, actorTrans.position);
        // FIXME: tổ chức lại cái này, để ở đây chưa hợp logic
        float MELEE_ATTACK_RANGE = 1;

        isGood =
            weaponHandler.CurWeapon is IRangedWeapon ||
            distance <= MELEE_ATTACK_RANGE;

        return isGood;
    }

    private void HandleTargetInteraction(GameObject target)
    {
        if (weaponHandler.CurWeapon == null) {
            return;
        }
        
        if (IsGoodToAttack(target))
        {
            PostControlCommand(ControlType.Attack);
        }
        else
        {
            PostControlCommand(ControlType.MoveByPos, (Vector2)target.transform.position); // move tới mục tiêu
        }
    }

    private Vector2 GetRandomWanderTarget()
    {
        Vector2 currentPosition = actorTrans.position; // lấy vị trí hiện tại của lính
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // tạo hướng ngẫu nhiên
        return currentPosition + randomDirection * wanderRadius; //  vị trí ngẫu nhiên trong bán kính di chuyển
    }
}
