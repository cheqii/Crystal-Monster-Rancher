using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AttackArea))]
public class AttackAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        AttackArea _attackArea = (AttackArea)target;
      
      
      
   
    }
}