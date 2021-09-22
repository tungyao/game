using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nakama;
using Script;

public class NakamaNet : MonoBehaviour
{
    // Start is called before the first frame update
    private string netScheme = "http";
    private string netHost = "172.27.112.234";
    private int netPort = 7350;
    private Client _client;

    async void Awake()
    {
        _client = new Client(netScheme, netHost, netPort, "defaultkey");
        var session = await _client.AuthenticateDeviceAsync(SystemInfo.deviceUniqueIdentifier);
        // 赋予全局变量
        var userData = new UserData
        {
            authToken = session.AuthToken,
            expireTime = session.ExpireTime,
            created = session.Created,
            userName = session.Username,
            userId = session.UserId
        };
        GlobalData.userData = userData;
        Debug.Log(GlobalData.userData);
    }
}