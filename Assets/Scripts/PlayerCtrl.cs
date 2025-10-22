using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCtrl : MonoBehaviour
{
    #region 角色血量
    public CharacterController charCtrl;
  #endregion 角色血量
    public int HP;
  
    Vector2 Input;
    Vector3 look;
    #region 生命週期
    // 初始化
    void Start()
    {
        HP = 100;
    }
    
    // 更新:偵測操作
    
    void Update()
    {
        Action();
        
    }
    #endregion 生命週期

    #region 操作設計
    void Action()
    {
        look.z = Input.y;    
        look.x = Input.x;
        transform.rotation = Quaternion.LookRotation(look);
    }

    public void Move(CallbackContext callback)
    {
        Input = callback.ReadValue<Vector2>();
        charCtrl.SimpleMove(Vector3.forward);
       
    }
    #endregion 操作設計

}


