using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerListUI : MonoBehaviourPunCallbacks
{
    public Transform content;                 // Контейнер для элементов сервера в ScrollView
    public GameObject serverItemPrefab;       // Префаб элемента сервера
    public Button refreshButton;              // Кнопка обновления списка

    private List<GameObject> serverItems = new List<GameObject>();  // Хранит все созданные элементы серверов

    private void Start()
    {
        refreshButton.onClick.AddListener(RefreshServerList);
    }

    // Обновление списка серверов с переданным списком данных серверов
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
            Debug.LogWarning("Не удалось обновить список серверов. Подключение отсутствует или игрок не находится в лобби.");
        }
    }

    // Обновление списка серверов при изменении данных о комнатах
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


// Структура данных сервера
[System.Serializable]
public class ServerData
{
    public string serverName;      // Имя сервера
    public int currentPlayers;     // Текущее количество игроков
    public int maxPlayers;         // Максимальное количество игроков

    public ServerData(string serverName, int currentPlayers, int maxPlayers)
    {
        this.serverName = serverName;
        this.currentPlayers = currentPlayers;
        this.maxPlayers = maxPlayers;
    }
}
