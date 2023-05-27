using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [Header("Icon Size")]
    [SerializeField] private int selectIconSize = 135;
    [SerializeField] private int normalIconSize = 75;

    [Header("Color Selected")] 
    [SerializeField] private Color selectColor;
    [SerializeField] private Color deSelectColor;
    
    private RectTransform _rectTransform;
    private Image slotImage;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        slotImage = transform.parent.parent.GetComponent<Image>();
    }

    public void Select()
    {
        _rectTransform.sizeDelta = new Vector2(selectIconSize, selectIconSize);
        slotImage.color = selectColor;
    }

    public void DeSelect()
    {
        _rectTransform.sizeDelta = new Vector2(normalIconSize, normalIconSize);
        slotImage.color = deSelectColor;
    }
}
