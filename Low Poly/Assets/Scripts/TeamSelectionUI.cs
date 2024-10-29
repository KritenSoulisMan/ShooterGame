using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionUI : MonoBehaviourPunCallbacks
{
    public GameObject teamSelectionPanel;
    public Button redTeamButton;
    public Button blueTeamButton;
    public Button randomTeamButton;

    public TMP_Text playersListTextRed;
    public TMP_Text playersListTextBlue;

    private List<string> redTeamPlayers = new List<string>();
    private List<string> blueTeamPlayers = new List<string>();

    private TeamManager teamManager;

    void Start()
    {
        teamManager = FindObjectOfType<TeamManager>();
        if (teamManager == null)
        {
            Debug.LogError("TeamManager не найден!");
            return;
        }

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            teamSelectionPanel.SetActive(true);
        }
        else
        {
            teamSelectionPanel.SetActive(false);
        }

        redTeamButton.onClick.AddListener(() => SelectTeam("RedTeam"));
        blueTeamButton.onClick.AddListener(() => SelectTeam("BlueTeam"));
        randomTeamButton.onClick.AddListener(SelectRandomTeam);
    }

    public void SelectTeam(string team)
    {
        teamManager.SetTeamAndSpawn(team);
        AddPlayerToTeamList(team);
        UpdatePlayersList();
        teamSelectionPanel.SetActive(false);
    }

    public void SelectRandomTeam()
    {
        string randomTeam = Random.value > 0.5f ? "RedTeam" : "BlueTeam";
        SelectTeam(randomTeam);
    }

    void AddPlayerToTeamList(string team)
    {
        if (team == "RedTeam")
        {
            redTeamPlayers.Add(PhotonNetwork.NickName);
        }
        else if (team == "BlueTeam")
        {
            blueTeamPlayers.Add(PhotonNetwork.NickName);
        }
    }

    void UpdatePlayersList()
    {
        playersListTextRed.text = "Red Team:\n" + string.Join("\n", redTeamPlayers);
        playersListTextBlue.text = "Blue Team:\n" + string.Join("\n", blueTeamPlayers);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayersList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        redTeamPlayers.Remove(otherPlayer.NickName);
        blueTeamPlayers.Remove(otherPlayer.NickName);
        UpdatePlayersList();
    }
}
