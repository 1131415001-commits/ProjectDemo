using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCtrl : MonoBehaviour
{
    #region 基本參數
    public CharacterController charCtrl;
    public Animator animator;

    public float moveSpeed = 3f;
    public float jumpHeight = 2f;

    public int HP;

    Vector2 input;
    Vector3 look;
    float speedV;
    #endregion

    #region 角色公開狀態
    public bool isMove => input != Vector2.zero;
    public bool isGrounded => charCtrl.isGrounded;

    public float G => 9.8f;

    public Vector3 Velocity =>
        transform.forward * moveSpeed * input.magnitude +
        Vector3.up * speedV;

    public float VelocityY => charCtrl.velocity.y;
    #endregion

    void Start()
    {
        HP = 100;
        speedV = 0f;
    }

    void Update()
    {
        Action();

        animator.SetBool("IsMove", isMove);
        animator.SetBool("IsGround", isGrounded);
        animator.SetFloat("BlendInput", input.magnitude);
        animator.SetFloat("VelocityY", VelocityY); // ? 正確
    }

    void Action()
    {
        // 移動方向
        look = new Vector3(input.x, 0, input.y);

        if (isMove)
            transform.rotation = Quaternion.LookRotation(look);

        // 落地時重置垂直速度
        if (isGrounded && speedV < 0)
            speedV = -2f;

        // 重力
        speedV -= G * Time.deltaTime;

        charCtrl.Move(Velocity * Time.deltaTime);
    }

    public void Move(CallbackContext callback)
    {
        input = callback.ReadValue<Vector2>();
    }

    public void Jump(CallbackContext callback)
    {
        if (isGrounded && callback.performed)
        {
            Debug.Log("從地面起跳");

            speedV = Mathf.Sqrt(2 * jumpHeight * G);
            animator.SetTrigger("Jump"); // ? 修正
        }
    }
}