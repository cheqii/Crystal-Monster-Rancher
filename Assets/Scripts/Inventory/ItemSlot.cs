using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private int selectIconSize = 135;
    [SerializeField] private int normalIconSize = 75;
    
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Select()
    {
        _rectTransform.sizeDelta = new Vector2(selectIconSize, selectIconSize);
    }

    public void DeSelect()
    {
        _rectTransform.sizeDelta = new Vector2(normalIconSize, normalIconSize);
    }
}
