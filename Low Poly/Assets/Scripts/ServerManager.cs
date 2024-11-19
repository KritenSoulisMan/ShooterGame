using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;    // Поле ввода для имени комнаты
    public TMP_InputField maxPlayersInput;  // Поле ввода для максимального количества игроков
    public ServerListUI serverListUI;       // Ссылка на скрипт ServerListUI

    public GameObject _createMenu;
    public GameObject _serverList;

    void Start()
    {
        ConnectToPhoton();
    }

    public void LookServerList()
    {
        _createMenu.SetActive(false);
        _serverList.SetActive(true);
    }

    public void LookCreateServer()
    {
        _createMenu.SetActive(true);
        _serverList.SetActive(false);
    }

    void ConnectToPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Попытка подключения к Photon серверу...");
        }
    }

    public void ConnectToRandomRoom()
    {
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            Debug.Log("Отсутствует подключение к Master Server. Ожидание подключения...");
        }
    }

    public void ConnectToNamedRoom()
    {
        if (!PhotonNetwork.IsConnectedAndReady || !PhotonNetwork.InLobby)
        {
            Debug.Log("Ожидание подключения к Master Server...");
            return;
        }

        string roomName = roomNameInput.text;
        byte maxPlayers = GetMaxPlayers();

        if (!string.IsNullOrEmpty(roomName))
        {
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayers };
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            Debug.Log("Создание или подключение к комнате с именем: " + roomName);
        }
        else
        {
            Debug.LogWarning("Пожалуйста, введите имя комнаты.");
        }
    }

    private byte GetMaxPlayers()
    {
        byte maxPlayers = 4;
        if (byte.TryParse(maxPlayersInput.text, out byte result))
        {
            maxPlayers = (byte)Mathf.Clamp(result, 2, 10);
        }
        return maxPlayers;
    }

    public void ReturnToMainMenu()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("lvl_2");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Подключение к Master Server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Подключение к лобби.");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        List<ServerData> serverDataList = new List<ServerData>();

        foreach (RoomInfo room in roomList)
        {
            if (room.PlayerCount > 0)  // Показать только активные комнаты
            {
                serverDataList.Add(new ServerData(room.Name, room.PlayerCount, room.MaxPlayers));
            }
        }

        serverListUI.UpdateServerList(serverDataList);  // Обновление UI списка серверов
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError("Ошибка подключения к случайной комнате.");
        byte maxPlayers = GetMaxPlayers();
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Успешное подключение к комнате: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene("lvl_1");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Покинул комнату.");
    }
}
