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
    public Turn PlayerBulletTracing;
    public Turn EnemyReceiveDamage;
    public Turn EnemyReady;
    public Turn EnemyShoot;
    public Turn EnemyBulletTracing;
    public Turn PlayerPerry;


    public KeyCode DemoStartKey;
  


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        else {
            Instance = this;
        }
    }

    void Start () {

	}

    void InitTurns(){
        Overviewing = new Turn(TurnType.Overviewing);
        PlayerReady = new Turn(TurnType.PlayerReady);
        PlayerShoot = new Turn(TurnType.PlayerShoot);
        PlayerBulletTracing = new Turn(TurnType.PlayerBulletTracing);
        EnemyReceiveDamage = new Turn(TurnType.EnemyReceiveDamage);
        EnemyReady = new Turn(TurnType.EnemyReady);
        EnemyShoot = new Turn(TurnType.EnemyShoot);
        EnemyBulletTracing = new Turn(TurnType.EnemyBulletTracing);
        PlayerPerry = new Turn(TurnType.PlayerPerry);

        Overviewing.nextTurn = PlayerReady;
        PlayerReady.nextTurn = PlayerShoot;
        PlayerShoot.nextTurn = PlayerBulletTracing;
        PlayerBulletTracing.nextTurn = EnemyReceiveDamage;
        EnemyReceiveDamage.nextTurn = EnemyReady;
        EnemyReady.nextTurn = EnemyShoot;
        EnemyShoot.nextTurn = EnemyBulletTracing;
        EnemyBulletTracing.nextTurn = PlayerPerry;
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
        if (Input.GetKeyDown(DemoStartKey)) {
            StopCoroutine(TurnHandlingCO());
            CurrentTurn = Overviewing;
            StartCoroutine(TurnHandlingCO());
        }
           
        if(Input.GetKeyDown (KeyCode.A))
        {
            
        }
	}

    IEnumerator TurnHandlingCO() {
        while (true) {
            HandleTurnEvents();
            yield return null;
        }
    }

    void UpdateTurnState() {

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


