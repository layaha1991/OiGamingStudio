using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour
{
    public delegate void Vector2Event(Vector2 vec);
    public static event Vector2Event OnTap;

    public delegate void HitEventHandler(float dmg);
    public static event HitEventHandler OnPlayerHit;

    public delegate void ParryEventHandler();
    public static event ParryEventHandler OnParry;

    public void CallOnTap(Vector2 vec)
    {
        if (OnTap != null)
        {
            OnTap(vec);
        }
    }


    public void CallOnPlayerHit(float dmg)
        {
            if (OnPlayerHit != null)
            {
                OnPlayerHit(dmg);
            }
        }

    public void CallOnParry()
    {
        if (OnParry!= null)
        {
            OnParry();
        }
    }
}
