#region

using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

#endregion

public class ConsoleToGui : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textZone;

    //#if !UNITY_EDITOR
    private string output;
    private string stack;

    StreamWriter writer;
    HttpClient client;
    string url;

    private void Awake()
    {

        url = "http://172.20.10.12:8080/log";
        client = new HttpClient();
        writer = new StreamWriter(Application.persistentDataPath + "/log.txt", true);
    }

    void OnEnable()
    {
        //Application.logMessageReceived += Log;
        //Application.logMessageReceived += Write;
        Application.logMessageReceived += async (string logString, string stackTrace, LogType type) => await Post(logString, stackTrace);
    }

    void OnDisable()
    {
        //Application.logMessageReceived -= Log;
        //Application.logMessageReceived -= Write;
        Application.logMessageReceived -= async (string logString, string stackTrace, LogType type) => await Post(logString, stackTrace);
    }

    private void OnDestroy()
    {
        writer.Close();
    }

    public void Log(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;
        textZone.text = output + "\n" + stackTrace + "\n" + textZone.text;
    }

    public void Write(string logString, string stackTrace, LogType type)
    {
        output = logString;
        stack = stackTrace;

        writer.WriteLine(output);
        writer.WriteLine(stackTrace);
    }

    public async Task Post(string logString, string stackTrace)
    {
        output = logString;
        stack = stackTrace;

        var text = output + "\n" + stack + "\n";
        var data = new StringContent(text, Encoding.UTF8);

        var response = await client.PostAsync(url, data);
    }
}
