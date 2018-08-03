using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventManager : MonoBehaviour {

    public delegate void FloatEvent(float f);
    public static event FloatEvent OnDamage;

    public void CallOnDamage(float dmg) {
        if (OnDamage!=null)
        {
            OnDamage(dmg);
        }
    }
}
