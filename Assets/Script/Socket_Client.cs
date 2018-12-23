using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Runtime;
using UnityEngine;

public class Socket_Client : MonoBehaviour
{

    static byte[] receiveBytes = new byte[1024];
    static Socket ClientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //List<Thread> thread_pool;
    // Use this for initialization
    private void Awake()
    {
        ConnectToServer();
    }

    static public void ConnectToServer()
    {
        int try_count = 0;
        while (!ClientSock.Connected)
        {
            try
            {
                try_count++;
                ClientSock.Connect(new IPEndPoint(IPAddress.Parse("192.168.0.3"), 9913)); //Loopback 대신 서버컴퓨터 IP 
            }
            catch (SocketException)
            {
                Debug.Log("서버가 닫혀있습니다. 시도횟수: " + try_count);
            }
        }
        Debug.Log("서버 접속 성공 ");
    }

    static public void Send_Receive_ToServer(string data)
    {
        byte[] buffer_send = Encoding.Default.GetBytes(data);
        ClientSock.Send(buffer_send);

        byte[] buffer_rec = new byte[1024];
        ClientSock.Receive(buffer_rec);
        ReceiveData( Encoding.Default.GetString(buffer_rec));
        
    }

    static public void ReceiveData(string data)
    {
        Debug.Log("서버로부터 받음 : " + data);
        string[] token = data.Split(';');
        if (token[0] == "NewAccount")
        {
            if (token[1] == "true")
            {
                Singletone_Manager.Singletone_Information.Success_NewAccount();
            }
            else if (token[1] == "false")
            {
                Singletone_Manager.Singletone_Information.Failed_NewAccount();
            }
        }

        else if(token[0] == "Login")
        {
            if (token[1] == "true")
            {
                //아이디 토큰 저장
                Singletone_Manager.Singletone_Information.Success_Login(token[2]);
            }
            else if (token[1] == "false")
            {
                Singletone_Manager.Singletone_Information.Failed_Login();
            }
        }
        else if (token[0] == "Data")
        {
            Singletone_Manager.Singletone_Information.Load_UserData(token[1]);           
        }
    }
}
#region tmp
/* static public void ReceiveFromServer()
{
    ClientSock.BeginReceive(receiveBytes, 0, 1024, SocketFlags.None,
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
}*/
#endregion