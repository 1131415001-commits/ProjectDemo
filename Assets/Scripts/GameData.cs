using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int hp = 100;
    public static int hpMax = 100;
    public static float hpFillAmount
    {
        get
        {
            return (float)hp / (float)hpMax;

        }

    }
}

