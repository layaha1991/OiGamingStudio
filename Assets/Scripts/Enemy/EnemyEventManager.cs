using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventManager : MonoBehaviour {

    public delegate void FloatEvent(float f);
    public static event FloatEvent OnDamage;


    public delegate void EnemyTurnHandler (bool isEnemyReady);
    public static event EnemyTurnHandler OnEnemyTurn;


    public void CallOnDamage(float dmg) {
        if (OnDamage!=null)
        {
            OnDamage(dmg);
        }
    }

    public void CallOnEnemyTurn(bool isEnemyReady)
    {
        if(OnEnemyTurn != null)
        {
            OnEnemyTurn(isEnemyReady);
        }
    }


}
