using UnityEngine;

public class LittleSoldierAI : IController
{
    private IActor actor; 
    private float idleTimer = 0f; //đếm thời gian trong trạng thái idle
    private float idleDuration = 2f; // thời gian chowff tromg trạng thái idle
    private Vector2 wanderTarget; // vị trí di chuyển ngẫu nhiên
    private const float attackRange = 2f; // tầm đánh
    private const float wanderRadius = 3f; // bán kính di chuyển ngẫu nhiên khi idle

    private void Awake()
    {
        actor = GetComponent<IActor>(); 
        if (actor == null)
        {
            enabled = false; 
        }
        
    }

    private void Update()
    {
        if (actor == null || actor.Stalker == null)
        {
            return;
        Debug.Log("IActor does not exist on " + gameObject.name);
        }

        // lấy mục tiêu top từ stalker
        GameObject target = actor.Stalker.TopTarget.Value;

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
            actor.MovementHandler.MoveByPos(wanderTarget); // điều khiển lính di chuyển tới vị trí mới
        }
        else
        {
            actor.BodyHandler.PlayAnim(ActorBodyHandler.Idle); // vẫn idel nếu chưa đủ time chờ
        }
    }

    private void HandleTargetInteraction(GameObject target)
    {
        // tính khoảng cách giữa soldier và mục tiêu
        float distance = Vector3.Distance(target.transform.position, actor.transform.position);

        if (distance > attackRange)
        {
            actor.MovementHandler.MoveByPos(target.transform.position); // move tới mục tiêu
        }
        else
        {
            actor.WeaponHandler.Attack(); // thực hiện tấn công nếu trong tầm đánh
        }
    }

    private Vector2 GetRandomWanderTarget()
    {
        Vector2 currentPosition = actor.transform.position; // lấy vị trí hiện tại của lính
        Vector2 randomDirection = Random.insideUnitCircle.normalized; // tạo hướng ngẫu nhiên
        return currentPosition + randomDirection * wanderRadius; //  vị trí ngẫu nhiên trong bán kính di chuyển
    }
}
