using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

public class JSLink : MonoBehaviour
{
    [Serializable]
    public class PlayerData
    {
        public string playerName;
        public string playerEmail;
        public string playerID;
        public string playerCompany;
    }

    public UnityAction OnGameDataImport;
    
    public PlayerData playerData;
    
    public static JSLink Instance {get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    [DllImport("__Internal")]
    private static extern void SendMessageToPlatform(string data);
    
    [UsedImplicitly]
    public void SendMessageToWebGL(string data)
    {
        // HANDLE DATA RECEIVED FROM UE5
        playerData = JsonConvert.DeserializeObject<PlayerData>(data);

        OnGameDataImport?.Invoke();
    }

    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SendMessageToPlatform("GAME_STARTED");
#endif
    }

    public void ExitGame(int score)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SendMessageToPlatform("GAME_EXIT with SCORE: " + score);
#endif        
    }

    public void SendScore(int score)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        SendMessageToPlatform("GAME_SCORE:" + score);
#endif          
    }
}
