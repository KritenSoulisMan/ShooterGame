using UnityEngine;

public class RoomData : MonoBehaviour
{
    public static RoomData Instance;
    public string RoomName { get; private set; }
    public byte MaxPlayers { get; private set; }

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

    public void SetRoomData(string roomName, byte maxPlayers)
    {
        RoomName = roomName;
        MaxPlayers = maxPlayers;
    }
}
