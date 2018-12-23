using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime;
using UnityEngine;

public class Socket_Client : MonoBehaviour {

    static byte[] receiveBytes = new byte[1080];
    static Socket ClientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
    List<Thread> thread_pool;
    // Use this for initialization
    private void Awake()
    {
        ConnectedToServer();
    }
    private void Update()
    {
        //if (ClientSock.Connected)
        //    Debug.Log("접속중");
    }
    static public void ConnectedToServer()
    {
        try
        {
            ClientSock.Connect(new IPEndPoint(IPAddress.Loopback, 9913));
        }
        catch
        {
            Debug.Log("서버가 닫혀있습니다.");
            Application.Quit();
        }
    }

    public void SendToServer(string data)
    {
        
        //루프백 대신 서버컴퓨터 IP         
        byte[] transferStr = Encoding.Default.GetBytes(data);
        ClientSock.BeginSend(transferStr, 0, transferStr.Length, SocketFlags.None,
                     new AsyncCallback(Send_Callback_Client), ClientSock);
        ReceiveFromServer();
        //루프백 대신 서버컴퓨터 IP         
    }

    static public void ReceiveFromServer()
    {
        ClientSock.BeginReceive(receiveBytes, 0, 1080, SocketFlags.None,
                     new AsyncCallback(Receive_callback_Client), ClientSock);        
    }

    static void Receive_callback_Client(IAsyncResult ar)
    {
        Socket transferSock = (Socket)ar.AsyncState;
        int strLength = transferSock.EndReceive(ar);
        if(strLength > 0)
        {
            Debug.Log("get_FullString : " + Encoding.Default.GetString(receiveBytes));
            string str = Encoding.Default.GetString(receiveBytes);
            string[] token_client = str.Split(';');

            if (token_client[0] == "Login")
            {
                if (token_client[1] == "true")
                    Singletone_Manager.Singletone_Information.Login_Success();
                else if (token_client[1] == "false")
                    Singletone_Manager.Singletone_Information.Login_Failed();
            }
        }
        ClientSock.EndReceive(ar);
    }

    static void Send_Callback_Client(IAsyncResult ar)
    {
        Socket transferSock = (Socket)ar.AsyncState;
        int strLength = transferSock.EndSend(ar);
        ClientSock.EndSend(ar);
        ReceiveFromServer();
    }
}
