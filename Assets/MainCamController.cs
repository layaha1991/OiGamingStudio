using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamController : MonoBehaviour {
    TurnManager turnManager;
    Animator animator;
	// Use this for initialization
	void Start () {
        turnManager = TurnManager.Instance;
        animator = GetComponent<Animator>();
        SetupEvents();
	}

    void SetupEvents() {
        turnManager.PlayerReady.OnStart += FocusPlayer;
        turnManager.PlayerPerry.OnStart += FocusPlayer;
        turnManager.PlayerBulletTracking.OnStart += TrackBullets;
        turnManager.EnemyBulletTracking.OnStart += TrackBullets;
        turnManager.EnemyReceiveDamage.OnStart += FocusEnemy;
        turnManager.EnemyReady.OnStart += FocusEnemy;
    }

    void ClearEvents()
    {
        turnManager.PlayerReady.OnStart -= FocusPlayer;
        turnManager.PlayerPerry.OnStart -= FocusPlayer;
        turnManager.PlayerBulletTracking.OnStart -= TrackBullets;
        turnManager.EnemyBulletTracking.OnStart -= TrackBullets;
        turnManager.EnemyReceiveDamage.OnStart -= FocusEnemy;
        turnManager.EnemyReady.OnStart -= FocusEnemy;
    }

    private void OnDestroy()
    {
        ClearEvents();    
    }

    void FocusPlayer() {
        animator.SetTrigger("PlayerFocused");
    }
    void FocusEnemy() {
        animator.SetTrigger("EnemyFocused");
    }
    void Overview() {
        animator.SetTrigger("Overviewing");
    }
    void TrackBullets() {
        animator.SetTrigger("BulletTracking");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
