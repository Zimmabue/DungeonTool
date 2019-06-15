using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLoader_Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        LoadSprite("oggetto1");
    }

    private void LoadSprite(string name)
    {
        var sprite = SpriteLoader.Instance.GetItem(name);
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            LoadSprite("oggetto1");

        if (Input.GetKeyDown(KeyCode.S))
            LoadSprite("oggetto2");
    }

}
