using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;

    private void Awake()
    {
        Instance = this as T;
    }
}
