using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralUI : MonoBehaviour {


    [SerializeField]
    private Text _levelText;

    private void Awake()
    {
        GameManager.OnLevelUp += CallOnLevelUp;
    }

    private void CallOnLevelUp()
    {
        LevelTextFunction();
    }

    private void LevelTextFunction()
    {
        _levelText.text = "Level: 1- " + GameManager.instance.level;
    }

	
}
