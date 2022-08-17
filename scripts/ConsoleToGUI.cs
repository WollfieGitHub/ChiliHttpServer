#region

using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

#endregion

public class ConsoleToGui : MonoBehaviour
{

    ClientWebSocket client;
    private bool serverNotConnectedMessageSent;

// //======================================================================================\\
// ||                                                                                      ||
// ||                                       MODIFY HERE                                    ||
// ||                                                                                      ||
// PUT THE IPV4 ADDRESS HERE SO THAT THE FORMAT BECOMES : "ws://<IPV4>:8080/log/Occulus%20Quest%202"
readonly string url = "ws://192.168.194.76:8080/log/Occulus%20Quest%202";
// ||                                                                                      ||
// \\======================================================================================//
    
    
    void OnEnable()
    {
        Application.logMessageReceived += Handle;
    }

    void Connect()
    {
        client.ConnectAsync(new Uri(url), CancellationToken.None);
    }
    
    void OnDisable()
    {
        Application.logMessageReceived -= Handle;
        client.CloseAsync(WebSocketCloseStatus.Empty, null, CancellationToken.None);
    }

    async void Handle(string logString, string stackTrace, LogType type)
    {
        try {
            await Post(logString, stackTrace);
            
        } catch (Exception e) {
            if (!serverNotConnectedMessageSent)
            {
                serverNotConnectedMessageSent = true;
                Debug.LogError($"The UDP Server is not connected on the specified url : {url}");
            }
        }
    }
    

    async Task Post(string logString, string stackTrace)
    {
        var text = logString + "\n" + stackTrace + "\n";

        await Send(text);
    }

    Task Send(string message)
    {
        if (ReferenceEquals(client, null))
        {
            client = new ClientWebSocket();
            try {
                Connect();
                
            } catch (Exception e) { Debug.LogError(e); }
        }

        Debug.Log($"Websocket : {client}");
        return client.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(message)),
            WebSocketMessageType.Text, true,
            CancellationToken.None);
        
    }
}
