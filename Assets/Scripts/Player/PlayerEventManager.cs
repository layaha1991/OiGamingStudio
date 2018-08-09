using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour {
    public delegate void Vector2Event(Vector2 vec);
    public static event Vector2Event OnTap;

    public void CallOnTap(Vector2 vec) {
        if (OnTap!=null)
        {
            OnTap(vec);
        }
    }
}
