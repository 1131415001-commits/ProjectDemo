using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyCountCtrl : MonoBehaviour
{
    public Image[] keys;

    [Header("顏色設定")]
    public Color gotColor = Color.white;   // 拿到
    public Color noneColor = Color.black;  // 尚未拿到

    void Start()
    {
        UpdateKeyUI();

        // 託管 UI 更新
        GameData.updateKey = UpdateKeyUI;

        // 開始提示
        if (UICutInCtrl.instance != null)
        {
            UICutInCtrl.instance.StartInfo();
        }
    }

    public void UpdateKeyUI()
    {
        int max = Mathf.Min(GameData.keyMax, keys.Length);

        for (int i = 0; i < max; i++)
        {
            if (i < GameData.keyCount)
            {
                keys[i].color = gotColor;   // 變亮
            }
            else
            {
                keys[i].color = noneColor;  // 變黑
            }
        }

        if (GameData.keyCount >= GameData.keyMax)
        {
            if (UICutInCtrl.instance != null)
            {
                UICutInCtrl.instance.EndInfo();
            }
        }
    }
}