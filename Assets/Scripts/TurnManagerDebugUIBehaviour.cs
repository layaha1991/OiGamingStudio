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


        debugText.text = turnManager.CurrentTurn.ToString();

    }
}
