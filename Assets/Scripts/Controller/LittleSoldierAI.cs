using UnityEngine;

public class LittleSoldierAI : ActorCommander {
    protected Transform Root => Actor.transform;
    protected IStalker Stalker => Actor.Stalker;
    protected WeaponHandler WeaponHandler => Actor.WeaponHandler;

    [SerializeField]
    [Tooltip("Thời gian chờ trong trạng thái idle")]
    private float idleDuration = 2f; // thời gian chowff tromg trạng thái idle
    [SerializeField]
    [Tooltip("Bán kính di chuyển ngẫu nhiên khi idle")]
    private const float wanderRadius = 3f; // bán kính di chuyển ngẫu nhiên khi idle

    private float idleTimer = 0f; //đếm thời gian trong trạng thái idle
    private Vector2 wanderTarget; // vị trí di chuyển ngẫu nhiên

    private void Update() {
        // lấy mục tiêu top từ stalker
        GameObject target = Stalker.TopTarget.Value;

        if (target == null) {
            HandleIdle();
        } else {
            HandleTargetInteraction(target);
        }
    }

    private void HandleIdle() {
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration) {
            idleTimer = 0f;
            wanderTarget = GetRandomWanderTarget(); //chon vị trí ngẫu nhiên
            PostCommand(ActorCommand.MoveByPos, wanderTarget);// điều khiển lính di chuyển tới vị trí mới
        } else {
            PostCommand(ActorCommand.Idle);// vẫn idel nếu chưa đủ time chờ
        }
    }

    /// <summary>
    /// FIXME: logic chưa chuẩn lắm
    /// </summary>
    private bool IsGoodToAttack(GameObject target) {
        bool isGood = false;

        // tính khoảng cách giữa soldier và mục tiêu
        float distance = Vector3.Distance(target.transform.position, Root.position);
        // FIXME: tổ chức lại cái này, để ở đây chưa hợp logic
        float MELEE_ATTACK_RANGE = 1;

        isGood =
            WeaponHandler.CurWeapon is IRangedWeapon ||
            distance <= MELEE_ATTACK_RANGE;

        return isGood;
    }

    private void HandleTargetInteraction(GameObject target) {
        if (WeaponHandler.CurWeapon == null) {
            return;
        }

        if (IsGoodToAttack(target)) {
            PostCommand(ActorCommand.Attack);
        } else {
            PostCommand(ActorCommand.MoveByDir, (Vector2)(target.transform.position - Root.position)); // move tới mục tiêu
        }
    }

    private Vector2 GetRandomWanderTarget() {
        Vector2 currentPosition = Root.position; // lấy vị trí hiện tại của lính
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // tạo hướng ngẫu nhiên
        return currentPosition + randomDirection * wanderRadius; //  vị trí ngẫu nhiên trong bán kính di chuyển
    }
}
