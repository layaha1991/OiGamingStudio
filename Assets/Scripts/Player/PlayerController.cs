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
            if (TurnManager.Instance.CurrentTurn == TurnManager.Instance.PlayerShoot)
            {
                playerEventManager.CallOnTap(new Vector2(0, 0));
            }
            else if (TurnManager.Instance.CurrentTurn == TurnManager.Instance.PlayerParry)
            {
                playerEventManager.CallOnParry();
            }
        }

    }
}
