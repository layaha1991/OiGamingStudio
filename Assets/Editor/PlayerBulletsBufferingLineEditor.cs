using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PlayerBulletsBufferingLine))]
public class PlayerBulletsBufferingLineEditor : Editor {
    private void OnSceneGUI()
    {
        PlayerBulletsBufferingLine playerBulletsBufferingLine = (PlayerBulletsBufferingLine)target;
        if (playerBulletsBufferingLine == null) {
            return;
        }

        Handles.color = Color.red;
        Handles.Label(playerBulletsBufferingLine.transform.position + Vector3.up * 2, "Player Bullet Buffering Line");
        Handles.BeginGUI();
        Handles.EndGUI();
    }
}
