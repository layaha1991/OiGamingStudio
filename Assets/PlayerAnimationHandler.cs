using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHandler : MonoBehaviour {

    List<Animator> bodyPartsAnimators;
    TurnManager turnManager;
	// Use this for initialization
	void Start () {
        bodyPartsAnimators = new List<Animator>();
		foreach(Animator anim in GetComponentsInChildren<Animator>()) {
            bodyPartsAnimators.Add(anim);
        }
        turnManager = TurnManager.Instance;
	}

    void SetupEvents() {
        turnManager.PlayerShoot.OnStart += RiseArmAndAim;

    }

    void ClearEvents() {
        turnManager.PlayerShoot.OnStart -= RiseArmAndAim;
    }
    private void OnDestroy()
    {
        
    }
    // Update is called once per frame
    void Update () { 
		
	}
    void RiseArmAndAim()
    {
        foreach (Animator anim in bodyPartsAnimators)
        {
            anim.SetTrigger("RiseNAim");
        }

    }
}
