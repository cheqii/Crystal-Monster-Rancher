using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CreateInputManager
{

    public static InventoryInputManager asset;

#if UNITY_EDITOR
    public static InventoryInputManager createInputManager()
    {
        asset = ScriptableObject.CreateInstance<InventoryInputManager>();

        AssetDatabase.CreateAsset(asset, "Assets/InventoryMaster/Resources/InputManager.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
#endif

}
