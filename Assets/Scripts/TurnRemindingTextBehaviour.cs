using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRemindingTextBehaviour : MonoBehaviour {

    TurnManager turnManager;
    Animator anim;
	// Use this for initialization
	void Start () {
        turnManager = TurnManager.Instance;
        anim = GetComponent<Animator>();
        SetupEvents();
	}

    void SetupEvents() {
        turnManager.PlayerReady.OnStart += OnPlayerReadyHandler;
        turnManager.EnemyReady.OnStart += OnEnemyReadyHandler;
        
    }
    private void OnDestroy()
    {
       
    }
    void DestroyEvents() {

    }

    void OnPlayerReadyHandler() {
        anim.SetTrigger("PlayerReady");
    }

    void OnPlayerWinHandler() {
        anim.SetTrigger("PlayerWin");
    }

    void OnPlayerLoseHandler() {
        anim.SetTrigger("PlayerLose");
    }

    void OnEnemyReadyHandler() {
        anim.SetTrigger("EnemyReady");
    }

    void OnPlayerReadyAnimationFinished() {
        if (turnManager.CurrentTurn == turnManager.PlayerReady)
        {
            turnManager.NextTurn();
        }
    }

    void OnEnemyReadyAnimationFinished()
    {
        if (turnManager.CurrentTurn == turnManager.EnemyReady)
        {
            turnManager.NextTurn();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
