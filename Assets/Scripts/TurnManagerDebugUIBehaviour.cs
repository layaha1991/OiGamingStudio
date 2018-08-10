using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManagerDebugUIBehaviour : MonoBehaviour 
{
    TurnManager turnManager;
    public Text debugText;

	void Start () {
        turnManager = TurnManager.Instance;
	}
	
	void Update () {
        if (turnManager.CurrentTurn != null)
        {
            debugText.text = "Current Turn : " + turnManager.CurrentTurn.type.ToString();
        }
        else {
            debugText.text = "Unknown Current Turn";
        }
    }
}
