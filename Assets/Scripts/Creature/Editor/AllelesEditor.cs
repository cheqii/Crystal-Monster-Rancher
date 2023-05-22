using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColorAlleles))]
public class AllelesEditor : Editor
{
   public override void OnInspectorGUI()
   {
      DrawDefaultInspector();
      ColorAlleles _alleles = (ColorAlleles)target;
      
      
      
      if (GUILayout.Button("Reload Color"))
      {
         _alleles.Start();
         _alleles.SetEffects();
      }
   }
}
