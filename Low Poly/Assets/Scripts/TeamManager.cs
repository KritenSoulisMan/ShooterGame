using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

public class TeamManager : MonoBehaviourPunCallbacks
{
    // Префаб игрока для красной и синей команды
    public GameObject redTeamPlayerPrefab;
    public GameObject blueTeamPlayerPrefab;

    // Область спавна игроков красной и синей команды
    public GameObject redTeamSpawnArea;
    public GameObject blueTeamSpawnArea;

    private string playerTeam;
    // Хранит ссылку на спавненного игрока
    private GameObject spawnedPlayer;           

    // Установка команды и спавн игрока
    public void SetTeamAndSpawn(string team)
    {
        playerTeam = team;
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        // Проверка, чтобы не спавнить игрока дважды
        if (spawnedPlayer != null)
        {
            Debug.LogWarning("Игрок уже заспавнен.");
            return;
        }

        // Убедимся, что спавним только локального игрока
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.IsMasterClient)
        {
            Debug.LogWarning("Игрок будет заспавнен только для локального клиента.");
            return;
        }

        GameObject selectedPrefab = playerTeam == "RedTeam" ? redTeamPlayerPrefab : blueTeamPlayerPrefab;

        if (selectedPrefab == null)
        {
            Debug.LogError($"Prefab для команды {playerTeam} не найден. Пожалуйста, установите его в инспекторе.");
            return;
        }

        // Определяем позицию для спавна в зависимости от команды
        Vector3 spawnPosition = GetSpawnPosition();

        // Спавн игрока
        spawnedPlayer = PhotonNetwork.Instantiate(selectedPrefab.name, spawnPosition, Quaternion.identity);

        // Добавляем компонент команды
        if (playerTeam == "RedTeam")
        {
            spawnedPlayer.AddComponent<RedTeam>();
        }
        else if (playerTeam == "BlueTeam")
        {
            spawnedPlayer.AddComponent<BlueTeam>();
        }

        // Устанавливаем родительский объект
        spawnedPlayer.transform.SetParent(this.transform);
    }

    Vector3 GetSpawnPosition()
    {
        GameObject spawnArea = playerTeam == "RedTeam" ? redTeamSpawnArea : blueTeamSpawnArea;
        Vector3 center = spawnArea.transform.position;
        Vector3 size = spawnArea.transform.localScale;

        float spawnX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float spawnY = center.y; /// Фиксация координаты Y ///
        float spawnZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(spawnX, spawnY, spawnZ);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Подключен к комнате. Выберите команду.");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Вышел из комнаты. Возвращение в лобби");
        // Очистка ссылки при выходе из комнаты
        spawnedPlayer = null;
    }
}
