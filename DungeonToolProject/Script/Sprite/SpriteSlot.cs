using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

[RequireComponent(typeof(Image))]
public class SpriteSlot : MonoBehaviour, IPointerClickHandler
{

    public delegate void OnClick(string sprite);
    public OnClick onClick;

    private Sprite _sprite;
    public Sprite sprite
    {
        get { return _sprite; }
        set
        {
            _sprite = value;
            GetComponent<Image>().sprite = value;
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
            onClick(sprite.name);
    }
}
