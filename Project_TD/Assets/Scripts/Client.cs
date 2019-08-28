using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour
{
    public static Client instance;
    [HideInInspector] public Socket client;
    Thread readingThread;
    public List<Perk> personalPerks = new List<Perk>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnLevelWasLoaded(int level)
    {
        if(Weapon_Equipper.instance != null)
        {
            if (personalPerks.Count > 0)
            {
                Weapon_Equipper.instance.AddNewPerks(personalPerks);
            }
        }
    }

    public void Connect(string ip)
    {
        Action<string> onMSGRecieved = OnMessageRecieved;
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            Debug.Log("Connecting!");
            client.Connect(IPAddress.Parse(ip), 35555);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        readingThread = new Thread(() =>
        {
            while (client.Connected)
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                int length = client.Receive(data);
                if (length > 0)
                {
                    string text = Encoding.UTF8.GetString(data).Trim().Replace("\0", "");
                    if (text.Contains("&"))
                    {
                        var splits = text.Split('&');
                        foreach (var item in splits)
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() => onMSGRecieved.Invoke(item));
                        }
                    }
                    else
                        UnityMainThreadDispatcher.Instance().Enqueue(() => onMSGRecieved.Invoke(text));
                }
            }
        });
        readingThread.Start();
    }

    void OnMessageRecieved(string s)
    {
        if (s == "%BeginGame")
        {
            Invoke("GameBeginInvoke", 1);
        }
        if(s.StartsWith("%RecieveBlocker"))
        {
            var splits = s.Split(' ');
            GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().SpawnBlocker(int.Parse(splits[1]));
        }
        if (s == "%GameWon")
        {
            SceneManager.LoadScene(0);
            Disconnect();
            Debug.Log("Won the Game!");
        }
        if (s.StartsWith("%ChangeToMap"))
        {
            var splits = s.Split(' ');
            SceneManager.LoadScene(int.Parse(splits[1]));
        }
    }

    void GameBeginInvoke()
    {
        GameManager.instance.BeginGame();
    }

    public void Send(string s)
    {
        if(client != null)
        {
            if (client.Connected)
            {
                client.Send(Encoding.UTF8.GetBytes(s));
            }
        }
    }

    public void Disconnect()
    {
        readingThread.Abort();
        Send("$LostGame");
    }
    
    private void OnApplicationQuit()
    {
        if(client != null)
        {
            Disconnect();
        }
    }
}
