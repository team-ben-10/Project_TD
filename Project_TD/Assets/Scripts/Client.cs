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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
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
        new Thread(() =>
        {
            while (client.Connected)
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                int length = client.Receive(data);
                if (length > 0)
                {
                    string text = Encoding.UTF8.GetString(data).Trim().Replace("\0", "");
                    UnityMainThreadDispatcher.Instance().Enqueue(() => onMSGRecieved.Invoke(text));
                }
            }
        }).Start();
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
            Client.instance.client.Disconnect(true);
            SceneManager.LoadScene(0);
            Debug.Log("Won the Game!");
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

    
    
    private void OnApplicationQuit()
    {
        if(client != null)
        {
            client.Disconnect(false);
            client.Dispose();
        }
    }
}
