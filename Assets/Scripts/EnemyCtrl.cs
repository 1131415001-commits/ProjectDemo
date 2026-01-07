using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    #region 基本參數
    public CharacterController charCtrl;
    public Transform player;

    [Header("移動參數")]
    public float moveSpeed = 2f;
    public float gravity = 9.8f;

    [Header("AI 距離設定")]
    public float searchDistance = 10f;   // 搜索範圍
    public float attackDistance = 2f;    // 攻擊距離

    [Header("Animator（子物件）")]
    public Animator animator;
    #endregion

    #region 狀態
    float speedV;
    float distance;
    #endregion

    void Update()
    {
        if (player == null) return;

        distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            Attack();
        }
        else if (distance <= searchDistance)
        {
            Chase();
        }
        else
        {
            Idle();
        }

        ApplyGravity();
    }

    #region 行為
    void Idle()
    {
        animator.SetBool("IsMove", false);
    }

    void Chase()
    {
        animator.SetBool("IsMove", true);

        Vector3 dir = player.position - transform.position;
        dir.y = 0;

        // 面向玩家
        transform.rotation = Quaternion.LookRotation(dir);

        // 移動
        Vector3 move = transform.forward * moveSpeed;
        charCtrl.Move(move * Time.deltaTime);
    }

    void Attack()
    {
        animator.SetBool("IsMove", false);
        animator.SetTrigger("Attack");
    }

    void ApplyGravity()
    {
        if (charCtrl.isGrounded && speedV < 0)
            speedV = -2f;

        speedV -= gravity * Time.deltaTime;
        charCtrl.Move(Vector3.up * speedV * Time.deltaTime);
    }
    #endregion
}
