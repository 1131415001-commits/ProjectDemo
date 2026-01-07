using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class EnemyCtrl : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;

    private Transform player;
    private CharacterController controller;
    private Animator animator;

    // 列舉AI的各種狀態
    private enum AIState
    {
        Idle,
        Chasing,
        Attacking
    }

    private AIState currentState = AIState.Idle;

    void Start()
    {
        // 獲取必要的組件
        controller = GetComponent<CharacterController>();
        
        // 從子物件中獲取Animator
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("在子物件中找不到Animator組件！");
        }

        // 透過標籤尋找玩家物件
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("找不到標籤為 'Player' 的物件！請確保您的玩家有正確的標籤。");
        }
    }

    void Update()
    {
        if (player == null)
        {
            // 如果找不到玩家，則不執行任何操作
            SetState(AIState.Idle);
            return;
        }

        // 計算與玩家的距離
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // 根據距離更新AI狀態
        if (distanceToPlayer <= attackRange)
        {
            SetState(AIState.Attacking);
        }
        else if (distanceToPlayer <= detectionRange)
        {
            SetState(AIState.Chasing);
        }
        else
        {
            SetState(AIState.Idle);
        }

        // 根據當前狀態執行對應的行為
        ExecuteCurrentState();
    }

    private void SetState(AIState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        
        // 根據新狀態更新動畫
        // 注意：動畫狀態的名稱 (例如 "Idle", "Run", "Attack") 必須與您的Animator Controller中的狀態名稱完全匹配。
        switch (currentState)
        {
            case AIState.Idle:
                animator.Play("Idle");
                break;
            case AIState.Chasing:
                animator.Play("Run");
                break;
            case AIState.Attacking:
                animator.Play("Attack");
                break;
        }
    }

    private void ExecuteCurrentState()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // 待機狀態下不執行任何操作
                break;
            case AIState.Chasing:
                // 追擊狀態
                ChasePlayer();
                break;
            case AIState.Attacking:
                // 攻擊狀態
                AttackPlayer();
                break;
        }
    }

    private void ChasePlayer()
    {
        // 計算朝向玩家的方向
        Vector3 direction = (player.position - transform.position).normalized;
        // 讓敵人總是面向玩家
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        
        // 應用重力
        Vector3 move = direction * moveSpeed;
        if (!controller.isGrounded)
        {
            move.y += Physics.gravity.y;
        }

        // 使用CharacterController移動敵人
        controller.Move(move * Time.deltaTime);
    }

    private void AttackPlayer()
    {
        // 攻擊時面向玩家
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
        // 攻擊動畫已在SetState中播放，這裡可以添加攻擊傷害邏輯
    }

    // 在編輯器中繪製可視化的範圍輔助線
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
