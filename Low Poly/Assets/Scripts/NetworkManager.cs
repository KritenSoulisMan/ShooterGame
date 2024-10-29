using Photon.Realtime;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ConnectToRoom()
    {
        string roomName = RoomData.Instance.RoomName;
        byte maxPlayers = RoomData.Instance.MaxPlayers;
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayers }, null);
    }

    public override void OnJoinedRoom()
    {
        SceneManager.LoadScene("lvl_1");
    }
}
