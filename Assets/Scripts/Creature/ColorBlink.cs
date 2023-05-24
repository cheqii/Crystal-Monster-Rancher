using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlink : MonoBehaviour
{
    public static ColorBlink Instance { get; private set; }
    
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void ObjectColorBlink(Renderer _renderer, Color _color,int blinkTimes)
    {
        StartCoroutine(Blink( _renderer,  _color,  blinkTimes));
    }
    
    
    
    public IEnumerator Blink(Renderer _renderer, Color _color,int blinkTimes)
    {
        if(_renderer.material.HasColor("_ColorBlink"))
        
        for (int i = 0; i < blinkTimes; i++)
        {
            _renderer.material.SetColor("_ColorBlink", _color);
            yield return new WaitForSeconds(0.1f);
            _renderer.material.SetColor("_ColorBlink", Color.black);
            yield return new WaitForSeconds(0.1f);

        }
        
    }
    
}
