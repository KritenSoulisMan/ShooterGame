using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerListUI : MonoBehaviourPunCallbacks
{
    public Transform content;                 // ��������� ��� ��������� ������� � ScrollView
    public GameObject serverItemPrefab;       // ������ �������� �������
    public Button refreshButton;              // ������ ���������� ������

    private List<GameObject> serverItems = new List<GameObject>();  // ������ ��� ��������� �������� ��������

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshServerList);
    }

    // ���������� ������ �������� � ���������� ������� ������ ��������
    public void UpdateServerList(List<ServerData> servers)
    {
        foreach (var item in serverItems)
        {
            Destroy(item);
        }
        serverItems.Clear();

        foreach (var server in servers)
        {
            GameObject newItem = Instantiate(serverItemPrefab, content);
            newItem.GetComponent<ServerItem>().Initialize(server);
            serverItems.Add(newItem);
        }
    }

    private void RefreshServerList()
    {
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InLobby)
        {
            PhotonNetwork.GetCustomRoomList(TypedLobby.Default, "");
        }
        else
        {
            Debug.LogWarning("�� ������� �������� ������ ��������. ����������� ����������� ��� ����� �� ��������� � �����.");
        }
    }

    // ���������� ������ �������� ��� ��������� ������ � ��������
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        List<ServerData> serverDataList = new List<ServerData>();

        foreach (var room in roomList)
        {
            if (!room.RemovedFromList)
            {
                ServerData serverData = new ServerData(room.Name, room.PlayerCount, room.MaxPlayers);
                serverDataList.Add(serverData);
            }
        }

        UpdateServerList(serverDataList);
    }
}


// ��������� ������ �������
[System.Serializable]
public class ServerData
{
    public string serverName;      // ��� �������
    public int currentPlayers;     // ������� ���������� �������
    public int maxPlayers;         // ������������ ���������� �������

    public ServerData(string serverName, int currentPlayers, int maxPlayers)
    {
        this.serverName = serverName;
        this.currentPlayers = currentPlayers;
        this.maxPlayers = maxPlayers;
    }
}
