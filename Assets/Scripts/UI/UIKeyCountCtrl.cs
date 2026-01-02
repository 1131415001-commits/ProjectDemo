using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyCountCtrl : MonoBehaviour
{
    public Image[ ] keys;
    public Color got;
    public Color none;

    // Start is called before the first frame update
    void Start()
    {
        UpdateKeyUI();

        GameData.updateKey = UpdateKeyUI;

UICutInCtrl.instance.StartInfo();
    }

   public void UpdateKeyUI()

    {
        for (int i = 0; i < GameData.keyMax; i++)
        {
            if (i < GameData.keyCount) keys[i].color = got;
            else keys[i].color = none;
        }
if (GameData.keyCount >= 3)
{
UICutInCtrl.instance.EndInfo();
    }
  }
}