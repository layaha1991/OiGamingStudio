using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TurnManagerDebugUIBehaviour : MonoBehaviour {
    TurnManager turnManager;
    Text debugText;
	// Use this for initialization
	void Start () {
        turnManager = TurnManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
        debugText.text = turnManager.CurrentTurn.type.ToString();
	}
}
