using Photon.Realtime;
using UnityEngine;
using Photon.Pun;

public class TeamManager : MonoBehaviourPunCallbacks
{
    // ������ ������ ��� ������� � ����� �������
    public GameObject redTeamPlayerPrefab;
    public GameObject blueTeamPlayerPrefab;

    // ������� ������ ������� ������� � ����� �������
    public GameObject redTeamSpawnArea;
    public GameObject blueTeamSpawnArea;

    private string playerTeam;
    // ������ ������ �� ����������� ������
    private GameObject spawnedPlayer;           

    // ��������� ������� � ����� ������
    public void SetTeamAndSpawn(string team)
    {
        playerTeam = team;
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        // ��������, ����� �� �������� ������ ������
        if (spawnedPlayer != null)
        {
            Debug.LogWarning("����� ��� ���������.");
            return;
        }

        // ��������, ��� ������� ������ ���������� ������
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.IsMasterClient)
        {
            Debug.LogWarning("����� ����� ��������� ������ ��� ���������� �������.");
            return;
        }

        GameObject selectedPrefab = playerTeam == "RedTeam" ? redTeamPlayerPrefab : blueTeamPlayerPrefab;

        if (selectedPrefab == null)
        {
            Debug.LogError($"Prefab ��� ������� {playerTeam} �� ������. ����������, ���������� ��� � ����������.");
            return;
        }

        // ���������� ������� ��� ������ � ����������� �� �������
        Vector3 spawnPosition = GetSpawnPosition();

        // ����� ������
        spawnedPlayer = PhotonNetwork.Instantiate(selectedPrefab.name, spawnPosition, Quaternion.identity);

        // ��������� ��������� �������
        if (playerTeam == "RedTeam")
        {
            spawnedPlayer.AddComponent<RedTeam>();
        }
        else if (playerTeam == "BlueTeam")
        {
            spawnedPlayer.AddComponent<BlueTeam>();
        }

        // ������������� ������������ ������
        spawnedPlayer.transform.SetParent(this.transform);
    }

    Vector3 GetSpawnPosition()
    {
        GameObject spawnArea = playerTeam == "RedTeam" ? redTeamSpawnArea : blueTeamSpawnArea;
        Vector3 center = spawnArea.transform.position;
        Vector3 size = spawnArea.transform.localScale;

        float spawnX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float spawnY = center.y; /// �������� ���������� Y ///
        float spawnZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(spawnX, spawnY, spawnZ);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("��������� � �������. �������� �������.");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("����� �� �������. ����������� � �����");
        // ������� ������ ��� ������ �� �������
        spawnedPlayer = null;
    }
}
