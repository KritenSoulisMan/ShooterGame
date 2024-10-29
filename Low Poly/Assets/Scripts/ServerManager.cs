using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;      // ���� ����� ��� ����� �������
    public TMP_InputField maxPlayersInput;    // ���� ����� ��� ������������� ���������� �������

    void Start()
    {
        // ������� ������������ � Photon ��� ������� �����
        ConnectToPhoton();
    }

    void ConnectToPhoton()
    {
        // ��������, ����� �� ������������ ������, ���� ��� ���� ����������
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();   // ����������� � Photon � ����������� �������
            Debug.Log("������� ����������� � Photon �������...");
        }
    }

    public void ConnectToRandomRoom()
    {
        // ��������, ��������� �� ����� � ������� � ��������� �� � �����
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRandomRoom();   // ����������� � ��������� �������
        }
        else
        {
            Debug.Log("����������� ����������� � Master Server. �������� �����������...");
        }
    }

    public void ConnectToNamedRoom()
    {
        // ��������, ������ �� ���������� � ��������� �� ����� � �����
        if (!PhotonNetwork.IsConnectedAndReady || !PhotonNetwork.InLobby)
        {
            Debug.Log("�������� ����������� � Master Server...");
            return;  // ����� �� ������, ���� ���������� ��� �� �����������
        }

        // ��������� ����� ������� �� ���� �����
        string roomName = roomNameInput.text;
        // ��������� ������������� ���������� ������� ��� �������
        byte maxPlayers = GetMaxPlayers();

        if (!string.IsNullOrEmpty(roomName))
        {
            // �������� ������� ��� ����������� � ������������ ������� � �������� ������
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayers };
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            Debug.Log("�������� ��� ����������� � ������� � ������: " + roomName);
        }
        else
        {
            Debug.LogWarning("����������, ������� ��� �������.");  // ��������������, ���� ��� ������� �� �������
        }
    }

    private byte GetMaxPlayers()
    {
        byte maxPlayers = 4;  // �������� �� ��������� - 4 ������
        // ������� ������������� �������� �� ���� ����� � byte � ���������� �������� �� 2 �� 10
        if (byte.TryParse(maxPlayersInput.text, out byte result))
        {
            maxPlayers = (byte)Mathf.Clamp(result, 2, 10);  // ������������ �� 2 �� 10 �������
        }
        return maxPlayers;
    }

    public void ReturnToMainMenu()
    {
        PhotonNetwork.LeaveRoom();  // ���������� �� ������� �������, ���� ��� ����������
        SceneManager.LoadScene("lvl_2");  // �������� ����� �������� ���� (lvl_2)
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("����������� � Master Server.");  // ������������� ��������� �����������
        PhotonNetwork.JoinLobby();   // ����������� � ����� ����� ����������� � Master Server
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("����������� � �����.");  // ������������� ��������� ����������� � �����
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError("������ ����������� � ��������� �������.");  // ��� ������, ���� �� ������� �������������� � ��������� �������
        byte maxPlayers = GetMaxPlayers();  // ��������� ������������� ���������� ������� ��� ����� �������
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });  // �������� ����� �������
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�������� ����������� � �������: " + PhotonNetwork.CurrentRoom.Name);  // �������� ����������� � �������
        SceneManager.LoadScene("lvl_1");  // ������� �� ������� ����� (lvl_1) ����� �����������
    }

    public override void OnLeftRoom()
    {
        Debug.Log("������� �������.");  // ��� ��� �������� ������ �� �������
    }
}
