using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryCtrl : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameData.hp = GameData.hpMax;
        }
    }
}

