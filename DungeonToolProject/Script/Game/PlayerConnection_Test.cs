using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerConnection_Test : NetworkBehaviour
{

    private WorldEntityNetwork _worldEntityNetwork;
    private Camera _camera;

    [SyncVar] private string _playerName = "";
    [SyncVar] private bool isMaster = false;

    // Use this for initialization
    IEnumerator Start()
    {
        if (isLocalPlayer)
        {
            FindObjectOfType<DiceThrowUI>().player = this;
            _camera = Camera.main;
            if (isServer)
            {
                isMaster = true;
            }
            _camera.GetComponent<MoveCamera>().enabled = isMaster;
            _camera.GetComponent<CameraFollow>().enabled = !isMaster;
            if (!isMaster)
            {
                while (_playerName == "")
                {
                    var localPlayer = Storage.StorageManager.GetDataAccess().Load<string>(Application.persistentDataPath + "/local.pl");
                    
                    if (localPlayer == null)
                    {
                        var sheet = CharacterSheet.Instance;
                        sheet.Activate();
                        sheet.player = this;
                        yield return null;
                    }
                    else
                    {
                        _playerName = localPlayer;
                        SetWorldEntity(localPlayer);
                        CmdSetWorldEntity(localPlayer);
                    }
                }
            }
        }

        if (_playerName != "")
        {
            if (isServer)
            {
                yield return null;
            }


            while(_worldEntityNetwork == null)
            {
                SetWorldEntity(_playerName);
                yield return null;
            }
            CmdSetReady(_playerName);
            InfoUI.Instance.View(_worldEntityNetwork, false);
            _camera.GetComponent<CameraFollow>().target = _worldEntityNetwork.transform;

        }
    }

    public void SendThrowDiceResult(string result)
    {
        if (isLocalPlayer)
        {
            if (isMaster)
                CmdSendMasterThrowDiceResult(result);
            else
                CmdSendPlayerThrowDiceResult(_playerName, result);
        }
    }
    
    [Command]
    private void CmdSendPlayerThrowDiceResult(string playerName, string result)
    {
        if (!GameServer.Instance.IsMyRound(playerName))
            return;

        RpcSendThrowDiceResult("<color=#000><b>" + playerName + "</b></color> throws:\n" + result);
    }

    [Command]
    private void CmdSendMasterThrowDiceResult(string result)
    {
        RpcSendThrowDiceResult("<color=#000><b>DM</b></color> throws:\n" + result);
    }

    [ClientRpc]
    private void RpcSendThrowDiceResult(string result)
    {
        FindObjectOfType<DiceThrowUI>().SetResult(result);
    }

    public void CreateNewPlayer(WorldEntity entity)
    {
        if (isLocalPlayer)
            CmdNewPlayer(entity);
    }
    
    [Command]
    private void CmdSetReady(string playerName)
    {
        GameServer.Instance.SetPlayer(playerName);
    }

    [Command]
    private void CmdNewPlayer(WorldEntity entity)
    {
        WorldServer.Instance.SpawnWorldEntity(entity);
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isMaster)
            {
                RaycastHit hit;
                if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    var we = hit.collider.GetComponent<WorldEntityNetwork>();
                    if (we != null)
                    {
                        _playerName = we.worldEntity.name;
                        CmdSetWorldEntity(_playerName);
                        InfoUI.Instance.View(we, true);
                    }
                }
            }
            if (!isMaster)
            {
                RaycastHit hit;
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    var we = hit.collider.GetComponent<WorldEntityNetwork>();
                    if (we != null)
                    {
                        InfoUI.Instance.View(we, false);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_playerName == "")
                return;
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            pos.x = Mathf.Floor(pos.x) + 0.5f;
            pos.y = Mathf.Floor(pos.y) + 0.5f;
            pos.z = 0;
            CmdSetPosition(_playerName, pos, isMaster);
        }

    }

    [Command]
    private void CmdSetPosition(string name, Vector3 position, bool hasMasterAutority)
    {
        if (!hasMasterAutority)
        {
            if (!GameServer.Instance.IsMyRound(name))
                return;
        }
        _worldEntityNetwork.Translate(position);
        RpcSetPosition(position);
    }

    [ClientRpc]
    private void RpcSetPosition(Vector3 position)
    {
        _worldEntityNetwork.Translate(position);
    }

    /*
    private void OnGUI()
    {
        if (isLocalPlayer)
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("zimmabue"))
            {
                _playerName = "zimmabue";
                SetWorldEntity("zimmabue");
                CmdSetWorldEntity("zimmabue");
                _camera.GetComponent<CameraFollow>().target = _worldEntityNetwork.transform;
            }

            if (GUILayout.Button("uriel"))
            {
                _playerName = "uriel";
                SetWorldEntity("uriel");
                CmdSetWorldEntity("uriel");
                _camera.GetComponent<CameraFollow>().target = _worldEntityNetwork.transform;
            }
            GUILayout.EndVertical();
        }
    }
    */

    [Command]
    private void CmdSetWorldEntity(string name)
    {
        SetWorldEntity(name);
        RpcSetWorldEntity(name);
    }

    [ClientRpc]
    private void RpcSetWorldEntity(string name)
    {
        SetWorldEntity(name);
    }

    private void SetWorldEntity(string name)
    {
        var wes = FindObjectsOfType<WorldEntityNetwork>();
        foreach (var we in wes)
        {
            if (we.worldEntity.name == name)
            {
                _worldEntityNetwork = we;
            }
        }
    }

}
