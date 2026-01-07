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
        // ¨¾§b¡GÁ×§K NullReferenceException
        if (hpBarImg == null) return;

        hpBarImg.fillAmount = GameData.hpFillAmount;
    }
}
