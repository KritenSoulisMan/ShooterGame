using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class ServerItem : MonoBehaviour
{
    public TMP_Text serverNameText;
    public TMP_Text playerCountText;

    private string roomName;

    public void Initialize(ServerData serverData)
    {
        roomName = serverData.serverName;
        serverNameText.text = serverData.serverName;
        playerCountText.text = $"{serverData.currentPlayers} / {serverData.maxPlayers}";
    }

    public void OnJoinServer()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
