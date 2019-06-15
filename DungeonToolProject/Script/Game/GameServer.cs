using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using Storage;

public class GameServer : NetworkBehaviour
{

    public static GameServer Instance = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public Canvas masterCanvas;
    public Transform playerListParent;
    public PlayerRoundSlot playerRoundSlotPrefab;
    
    private int currentRoundIndex;
    private List<string> _players;
    private List<PlayerRoundSlot> _playerRoundUI;

    // Use this for initialization
    void Start()
    {
        if (!isServer)
            return;
        masterCanvas.enabled = true;
        currentRoundIndex = 0;
        _players = new List<string>();
        _playerRoundUI = new List<PlayerRoundSlot>();
    }
    
    public void SetPlayer(string name)
    {
        if (_players.Contains(name))
            return;
        _players.Add(name);

        var pls = Instantiate(playerRoundSlotPrefab, playerListParent);
        pls.SetPlayer(name, IsMyRound(name));
        _playerRoundUI.Add(pls);
    }

    public bool IsMyRound(string name)
    {
        if (_players.Count == 0)
            return false;
        return name.Equals(_players[currentRoundIndex]);
    }
    
    public void UpdateRound()
    {
        currentRoundIndex = (currentRoundIndex + 1) % _players.Count;
        _playerRoundUI.ForEach(x => x.SetRound(false));
        _playerRoundUI[currentRoundIndex].SetRound(true);
        WorldServer.Instance.Save();
    }
    
    /*
    private void OnGUI()
    {
        if (!isServer)
            return;

        
        if (GUI.Button(new Rect(100, 0, 100, 30), "Next Round"))
            UpdateRound();
            
        if (GUI.Button(new Rect(100, 40, 100, 30), "Save"))
            WorldServer.Instance.Save();
        
        GUI.color = Color.white;
        if (_players.Count > 0)
            GUI.Label(new Rect(0, 50, 100, 50), _players[0]);
        else
            GUI.Label(new Rect(0, 50, 100, 50), "non presente");
            
    }*/

}
