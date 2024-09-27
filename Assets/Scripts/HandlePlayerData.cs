using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandlePlayerData : MonoBehaviour
{
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text id;
    [SerializeField] private TMP_Text email;
    [SerializeField] private TMP_Text company;

    private void Start()
    {
        JSLink.Instance.OnGameDataImport += UpdatePlayerInfo;
    }

    private void UpdatePlayerInfo()
    {
        name.text = "Player Name: " + JSLink.Instance.playerData.playerName;
        email.text = "Player Email: " + JSLink.Instance.playerData.playerEmail;
        id.text = "Player ID: " + JSLink.Instance.playerData.playerID;
        company.text = "Player Company: " + JSLink.Instance.playerData.playerCompany;
    }
}
