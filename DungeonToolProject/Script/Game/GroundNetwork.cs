using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class GroundNetwork : NetworkBehaviour
{

    [SyncVar] public string spriteName;
    
    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = SpriteLoader.Instance.GetSprite(spriteName);
    }
    
}
