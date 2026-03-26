using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;

public class UdpReceiver : MonoBehaviour
{
    UdpClient client;
    IPEndPoint receiver;

    bool isListening = true;

    public byte[] bytes;
    public bool isHandData = false;

    void Start()
    {
        Debug.Log("start");
        client = new UdpClient(50000);
        receiver = new IPEndPoint(IPAddress.Any, 0);

        ReceiveLoop();
    }

    async void ReceiveLoop()
    {
        isHandData = false;
        while (isListening)
        {
            try
            {
                UdpReceiveResult ur = await client.ReceiveAsync();
                
                isHandData = true;
                bytes = ur.Buffer;
                //Debug.Log(bytes.Length);
  
            }
            catch (Exception e) 
            {
                Debug.Log(e.ToString());
            }
            finally
            {

            }
            
        }
    }
    public void OnApplicationQuit()
    {
        isListening = false;
        client.Close();
    }
}

    

