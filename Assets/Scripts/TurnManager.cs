using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    public static TurnManager Instance;
    
    public Turn CurrentTurn;
    Turn OldTurn;

    public Turn Overviewing;
    public Turn PlayerReady;
    public Turn PlayerShoot;
    public Turn PlayerBulletTracking;
    public Turn EnemyReceiveDamage;
    public Turn EnemyReady;
    public Turn EnemyShoot;
    public Turn EnemyBulletTracking;
    public Turn PlayerPerry;

    public KeyCode DemoStartTestKey;
    public KeyCode DemoNextTurnKey;
  


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        else {
            Instance = this;
        }
        InitTurns();
    }

    void Start () {
        CurrentTurn = Overviewing;
	}

    void InitTurns(){
        Overviewing = new Turn(TurnType.Overviewing);
        PlayerReady = new Turn(TurnType.PlayerReady);
        PlayerShoot = new Turn(TurnType.PlayerShoot);
        PlayerBulletTracking = new Turn(TurnType.PlayerBulletTracing);
        EnemyReceiveDamage = new Turn(TurnType.EnemyReceiveDamage);
        EnemyReady = new Turn(TurnType.EnemyReady);
        EnemyShoot = new Turn(TurnType.EnemyShoot);
        EnemyBulletTracking = new Turn(TurnType.EnemyBulletTracing);
        PlayerPerry = new Turn(TurnType.PlayerPerry);

        Overviewing.nextTurn = PlayerReady;
        PlayerReady.nextTurn = PlayerShoot;
        PlayerShoot.nextTurn = PlayerBulletTracking;
        PlayerBulletTracking.nextTurn = EnemyReceiveDamage;
        EnemyReceiveDamage.nextTurn = EnemyReady;
        EnemyReady.nextTurn = EnemyShoot;
        EnemyShoot.nextTurn = EnemyBulletTracking;
        EnemyBulletTracking.nextTurn = PlayerPerry;
        PlayerPerry.nextTurn = PlayerReady;
    }
    public void NextTurn() {
        if (CurrentTurn != null)
        {
            CurrentTurn = CurrentTurn.nextTurn;
        }
        else {
            Debug.LogWarning("Trying to call NextTurn() when there is no Current Turn yet.");
        }
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(DemoStartTestKey)) {
            CurrentTurn = Overviewing;
        }
           
        if(Input.GetKeyDown (DemoNextTurnKey))
        {
            NextTurn();
        }
        HandleTurnEvents();
    }

    void HandleTurnEvents() {
        if (OldTurn != CurrentTurn) {
            if (OldTurn != null)
            {
                OldTurn.Finish();
            }

            if (CurrentTurn != null)
            {
                CurrentTurn.Start();
            }
        }
        
        OldTurn = CurrentTurn;
    }
}


public enum TurnType 
{
    Default,Overviewing,PlayerReady,PlayerShoot,PlayerBulletTracing, EnemyReady,EnemyReceiveDamage, EnemyShoot,EnemyBulletTracing,PlayerPerry
}

public class Turn {
    public TurnType type;
    public event VoidEvent OnStart;
    public event VoidEvent OnFinish;
    public Turn nextTurn;
    public Turn(TurnType t) {
        type = t;
    }
    public void Finish() {
        if (OnFinish != null) {
            OnFinish();
        }
        Debug.Log("Turn " + type.ToString() + " has finished.");
    }
    public void Start()
    {
        Debug.Log("Turn " + type.ToString() + " is Starting.");
        if (OnStart != null) {
            OnStart();
        }
    }
}

public delegate void VoidEvent();


