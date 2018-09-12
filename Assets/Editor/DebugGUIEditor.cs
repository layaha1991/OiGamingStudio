using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(DebugGUI))]
public class DebugGUIEditor : Editor {
    bool bulletTime = false;
    string bulletTimeText = "Bullet Time";
    private void OnSceneGUI()
    {
        DebugGUI debugGUI = (DebugGUI)target;
        if (debugGUI == null) {
            return;
        }
        Handles.color = Color.blue;
        Handles.BeginGUI();
        if (GUILayout.Button(bulletTime?"Bullet Time : ON":"Bullet Time : OFF",GUILayout.MaxWidth(200f))) {
            bulletTime = !bulletTime;
            if (bulletTime)
            {
                Time.timeScale = 0.1f;
            }
            else {
                Time.timeScale = 1f;
            }
        }
        Handles.EndGUI();
    }
}
