using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class EntityContainer : MonoBehaviour
{
    
    public WorldEntity worldEntity { get; set; }
    public Action onClick { get; set; }
    public Action onDrag { get; set; }
    
    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = SpriteLoader.Instance.GetSprite(worldEntity.spriteName);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (onClick != null)
            onClick();

        //aggiorna la UI
    }

    private void OnMouseDrag()
    {
        if (onDrag != null)
            onDrag();

        //posizione in base al mouse
        //aggiorna we position
    }

}
