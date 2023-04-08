using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorScripting : EditorWindow
{
    int coinCount = 0;

    [MenuItem("Tools / Add Coins")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(EditorScripting));
    }

    private void OnGUI()
    {
        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        coinCount = EditorGUILayout.IntField("Coin Count", coinCount);
        GUI.backgroundColor = Color.red;

        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if(GUILayout.Button("Reset", GUILayout.Width(100), GUILayout.Height(30)))
        {
            reset();
        }
       
        if (GUILayout.Button("Apply", GUILayout.Width(100), GUILayout.Height(30)))
        {
            CoinBag.IncreaseCoinCount(coinCount);
            reset();
        }
        
        EditorGUILayout.EndHorizontal();
    }

    private void reset()
    {
        coinCount = 0;
    }
}
