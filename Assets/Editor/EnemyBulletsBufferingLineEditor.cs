using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EnemyBulletsBufferingLine))]
public class EnemyBulletsBufferingLineEditor : Editor {
    private void OnSceneGUI()
    {
        EnemyBulletsBufferingLine enemyBulletsBufferingLine = (EnemyBulletsBufferingLine)target;
        if (enemyBulletsBufferingLine == null) {
            return;
        }

        Handles.color = Color.red;
        Handles.Label(enemyBulletsBufferingLine.transform.position + Vector3.up * 2, "Enemy Bullet Buffering Line");
        Handles.BeginGUI();
        Handles.EndGUI();
    }
}
