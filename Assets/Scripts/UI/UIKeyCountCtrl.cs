using UnityEngine;
using UnityEngine.UI;

public class UIKeyCountCtrl : MonoBehaviour
{
    public Image[] keys;
    public Color gotColor = Color.white;
    public Color noneColor = Color.black;

    private bool endInfoPlayed = false;

    void Start()
    {
        Debug.Log("UIKeyCountCtrl Start");

        UpdateKeyUI();
        GameData.updateKey = UpdateKeyUI;

        if (UICutInCtrl.instance != null)
            UICutInCtrl.instance.StartInfo();
        else
            Debug.LogError("找不到 UICutInCtrl.instance");
    }

    public void UpdateKeyUI()
    {
        Debug.Log($"UpdateKeyUI，被呼叫，keyCount = {GameData.keyCount}");

        int max = Mathf.Min(GameData.keyMax, keys.Length);

        for (int i = 0; i < max; i++)
        {
            keys[i].color = (i < GameData.keyCount) ? gotColor : noneColor;
        }

        if (!endInfoPlayed && GameData.keyCount >= GameData.keyMax)
        {
            endInfoPlayed = true;

            Debug.Log("鑰匙達標，呼叫 EndInfo");

            if (UICutInCtrl.instance != null)
                UICutInCtrl.instance.EndInfo();
            else
                Debug.LogError("EndInfo 時找不到 UICutInCtrl.instance");
        }
    }
}