using System;
using UnityEngine;

public static class GameData
{
    public static int hp = 100;
    public static int hpMax = 100;

    public static float hpFillAmount
    {
        get { return (float)hp / hpMax; }
    }

    public static int keyCount = 0;
    public static int keyMax = 3;

    public static Action updateKey;

    public static void AddKey()
    {
        keyCount++;

        Debug.Log($"AddKey ©I¥s¡AkeyCount = {keyCount}");

        if (updateKey != null)
            updateKey();
        else
            Debug.LogWarning("updateKey ©|¥¼³Qµù¥U");
    }
}