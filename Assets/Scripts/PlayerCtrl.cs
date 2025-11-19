using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCtrl : MonoBehaviour
{
    #region 基本參數
    public CharacterController charCtrl;
    public Animator animator;
    public float moveSpeed = 3f;
    
    public float jumpHeight = 2f;
    
    ///<summary>
    ///角色的血量
    ///</summary>
    public int HP;
    ///<summary>
    ///搖桿外部輸入
    ///</summary>
    Vector2 Input;
    Vector3 look;
    float speedV;
    #endregion 基本參數

    #region 角色公開狀態
    ///<summary>
    ///角色是否有接收輸入操作
    ///</summary>
    public bool isMove => Input != Vector2.zero;

    public bool isGrounded => charCtrl.isGrounded;

    public float G => 9.8f;
    public Vector3 Velocity => transform.forward * moveSpeed * Input.magnitude + Vector3.up * speedV;

    #endregion 角色公開狀態

    #region UNITY生命週期
    // 初始化
    void Start()
    {
        HP = 100;
    }
    
    // 更新:偵測操作
    void Update()
    {
        Action();
        animator.SetBool("IsMove", isMove);
        animator.SetFloat("BlendInput", Input.magnitude);
    }
    #endregion UNITY生命週期

    #region 操作設計
    void Action()
    {
        //轉動角色
        look.z = Input.y;    
        look.x = Input.x;
       
        
            //有移動操作產生時角色控制框轉向操作方向
            if (isMove) transform.rotation = Quaternion.LookRotation(look);
         
        
        //地心引力
        speedV -= G * Time.deltaTime;
        charCtrl.Move(Velocity * Time.deltaTime);
    }

    public void Move(CallbackContext callback)
    {
        Input = callback.ReadValue<Vector2>();
        Debug.Log(Input);
       
    }
    public void Jump(CallbackContext callback)
    { 
    if (isGrounded && callback.performed)
        {
            Debug.Log("從地面起跳");
            speedV = Mathf.Sqrt(2 * jumpHeight * G);
           
        }
    }
    #endregion 操作設計
}
