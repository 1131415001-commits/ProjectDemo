using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCtrl : MonoBehaviour
{
    #region 基本參數
    public CharacterController charCtrl;
    public Animator animator;
    ///<summary>
    ///角色的血量
    ///</summary>
    public int HP;
    ///<summary>
    ///搖桿外部輸入
    ///</summary>
    Vector2 Input;
    Vector3 look;
    #endregion 基本參數

    #region 角色公開狀態
    ///<summary>
    ///角色是否有接收輸入操作
    ///</summary>
    public bool isMove => Input != Vector2.zero;
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
    }
    #endregion UNITY生命週期

    #region 操作設計
    void Action()
    {
        //轉動角色
        look.z = Input.y;    
        look.x = Input.x;
        transform.rotation = Quaternion.LookRotation(look);
         //角色控制器.移動(往前)
        if (isMove) charCtrl.SimpleMove(transform.forward);
    }

    public void Move(CallbackContext callback)
    {
        Input = callback.ReadValue<Vector2>();
       
       
    }
    #endregion 操作設計
}
