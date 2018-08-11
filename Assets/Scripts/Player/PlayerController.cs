using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public KeyCode pcTapKey;
    PlayerEventManager playerEventManager;

    private void Awake()
    {
        playerEventManager = GetComponent<PlayerEventManager>();
    }
    void Update () {
        HandleInput();	
	}

    void HandleInput() {
        if (Input.GetKeyDown(pcTapKey)) 
        {
            playerEventManager.CallOnTap(new Vector2(0,0));
        }
        else if (Input.GetKeyDown(pcTapKey))
        {
            playerEventManager.CallOnParry();
            Debug.Log("detect space");
        }
    }
}
