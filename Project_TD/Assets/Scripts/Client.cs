using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static Client instance;
    Socket client;
    void Start()
    {
        instance = this;
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,ProtocolType.Tcp);
        try
        {
            Debug.Log("Connecting!");
            client.Connect(IPAddress.Parse("127.0.0.1"), 35555);
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
                    //We got Data
                    Debug.Log(Encoding.UTF8.GetString(data));
                }
            }
        }).Start();
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
