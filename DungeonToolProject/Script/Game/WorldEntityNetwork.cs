using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class WorldEntityNetwork : NetworkBehaviour
{

    public delegate void OnWorldEntityChange();
    public OnWorldEntityChange onWorldEntityChange;

    public float lerpRate;
    [SyncVar] public WorldEntity worldEntity;
    
    // Use this for initialization
    void Start()
    {
        
        GetComponent<SpriteRenderer>().sprite = SpriteLoader.Instance.GetSprite(worldEntity.spriteName);
        
    }
    
    [Command]
    private void CmdUpdateWorldEntity()
    {
        RpcUpdateWorldEntity(worldEntity);
    }

    [ClientRpc]
    private void RpcUpdateWorldEntity(WorldEntity we)
    {
        worldEntity = we;
        if (onWorldEntityChange != null)
            onWorldEntityChange();
    }

    public void UpdateWorldEntity()
    {
        CmdUpdateWorldEntity();
    }

    public void ChangeName(string name)
    {
        worldEntity.name = name;
    }

    private void OnMouseDown()
    {
        Debug.Log(worldEntity.name);
    }

    public void Translate(Vector3 position)
    {
        
        worldEntity.position = new Vector2(Mathf.Floor(position.x), Mathf.Floor(position.y));
        StartCoroutine(LerpPosition());
    }

    public void Position(Vector3 position)
    {
        worldEntity.position = position;
        transform.position = position + Vector3.back;
    }

    private IEnumerator LerpPosition()
    {
        while (Vector3.Distance(transform.position, (Vector3)(worldEntity.position + Vector2.one * 0.5f) + Vector3.back) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, (Vector3)(worldEntity.position + Vector2.one * 0.5f) + Vector3.back, Time.deltaTime * lerpRate);
            yield return null;
        }
        transform.position = (Vector3)(worldEntity.position + Vector2.one * 0.5f) + Vector3.back;
    }

}
