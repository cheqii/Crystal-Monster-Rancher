using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Creature))]
public class CreatureEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        GuiLine();

    }







    void GuiLine( int i_height = 1 )

    {

        Rect rect = EditorGUILayout.GetControlRect(false, i_height );

        rect.height = i_height;

        EditorGUI.DrawRect(rect, new Color ( 0.5f,0.5f,0.5f, 1 ) );

    }
}