using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBarCtrl : MonoBehaviour
{
    [Header("¦å±øUI¤¸¥ó")]
    public Image hpBarImg;

    void Update()
    {
        hpBarImg.fillAmount = GameData.hpFillAmount;
    }
}