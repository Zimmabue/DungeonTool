using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

public class NetworkManager_Custom : NetworkManager {

	
    // Use this for initialization
	void Start () {
        GameObject.Find("IPtext").GetComponent<Text>().text = "IP: " + LocalIPAddress();
	}

    public void StartupHost()
    {
        SetPort();
        NetworkManager.singleton.networkAddress = LocalIPAddress();
        NetworkManager.singleton.StartHost();
    }



    
    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }


    public void SetIPAddress()
    {
        string ipAddress = GameObject.Find("InputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        
        NetworkManager.singleton.networkAddress = ipAddress; 
        
    }

    public string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
    
    /*
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label(LocalIPAddress());
        GUILayout.EndVertical();
    }
    */

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;

    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 0)
        {
            SetupMenuSceneButtons();
        }
        else
        {
            SetupOtherSceneButtons();
        }

    }

    void SetupMenuSceneButtons()
    {
        GameObject.Find("IPtext").GetComponent<Text>().text = "IP: " + LocalIPAddress();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartupHost);

        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonJoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);
    }


    void SetupOtherSceneButtons()
    {
        //GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        //GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }

    
}
