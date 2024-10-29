using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ServerManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;      // Поле ввода для имени комнаты
    public TMP_InputField maxPlayersInput;    // Поле ввода для максимального количества игроков

    void Start()
    {
        // Попытка подключиться к Photon при запуске сцены
        ConnectToPhoton();
    }

    void ConnectToPhoton()
    {
        // Проверка, чтобы не подключаться дважды, если уже есть соединение
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();   // Подключение к Photon с настройками проекта
            Debug.Log("Попытка подключения к Photon серверу...");
        }
    }

    public void ConnectToRandomRoom()
    {
        // Проверка, подключен ли игрок к серверу и находится ли в лобби
        if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRandomRoom();   // Подключение к случайной комнате
        }
        else
        {
            Debug.Log("Отсутствует подключение к Master Server. Ожидание подключения...");
        }
    }

    public void ConnectToNamedRoom()
    {
        // Проверка, готово ли соединение и находится ли игрок в лобби
        if (!PhotonNetwork.IsConnectedAndReady || !PhotonNetwork.InLobby)
        {
            Debug.Log("Ожидание подключения к Master Server...");
            return;  // Выход из метода, если соединение еще не установлено
        }

        // Получение имени комнаты из поля ввода
        string roomName = roomNameInput.text;
        // Установка максимального количества игроков для комнаты
        byte maxPlayers = GetMaxPlayers();

        if (!string.IsNullOrEmpty(roomName))
        {
            // Создание комнаты или подключение к существующей комнате с заданным именем
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayers };
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            Debug.Log("Создание или подключение к комнате с именем: " + roomName);
        }
        else
        {
            Debug.LogWarning("Пожалуйста, введите имя комнаты.");  // Предупреждение, если имя комнаты не введено
        }
    }

    private byte GetMaxPlayers()
    {
        byte maxPlayers = 4;  // Значение по умолчанию - 4 игрока
        // Попытка преобразовать значение из поля ввода в byte и ограничить диапазон от 2 до 10
        if (byte.TryParse(maxPlayersInput.text, out byte result))
        {
            maxPlayers = (byte)Mathf.Clamp(result, 2, 10);  // Ограничиваем от 2 до 10 игроков
        }
        return maxPlayers;
    }

    public void ReturnToMainMenu()
    {
        PhotonNetwork.LeaveRoom();  // Отключение от текущей комнаты, если она существует
        SceneManager.LoadScene("lvl_2");  // Загрузка сцены главного меню (lvl_2)
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Подключение к Master Server.");  // Подтверждение успешного подключения
        PhotonNetwork.JoinLobby();   // Подключение к лобби после подключения к Master Server
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Подключение к лобби.");  // Подтверждение успешного подключения к лобби
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.LogError("Ошибка подключения к случайной комнате.");  // Лог ошибки, если не удалось присоединиться к случайной комнате
        byte maxPlayers = GetMaxPlayers();  // Получение максимального количества игроков для новой комнаты
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayers });  // Создание новой комнаты
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Успешное подключение к комнате: " + PhotonNetwork.CurrentRoom.Name);  // Успешное подключение к комнате
        SceneManager.LoadScene("lvl_1");  // Переход на игровую сцену (lvl_1) после подключения
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Покинул комнату.");  // Лог при успешном выходе из комнаты
    }
}
