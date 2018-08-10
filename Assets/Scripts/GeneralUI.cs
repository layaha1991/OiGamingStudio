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

    private void CallOnLevelUp(int level)
    {
        LevelTextFunction(level);
    }

    private void LevelTextFunction(int level)
    {
        _levelText.text = "Level: " + level;
    }

	
}
