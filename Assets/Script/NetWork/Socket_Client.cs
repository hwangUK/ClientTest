using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Socket_Client : MonoBehaviour
{
    static byte[] receiveBytes = new byte[1024];
    static Socket MySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private void Awake()
    {
        Thread th_main = new Thread(new ThreadStart(ConnectToServer));
        th_main.Start();
        //ConnectToServer();
    }

    static public void ConnectToServer()
    {
        int try_count = 0;
        while (!MySocket.Connected)
        {
            try
            {
                try_count++;
                MySocket.Connect(new IPEndPoint(IPAddress.Loopback, 9913)); //Loopback 대신 서버컴퓨터 IP IPAddress.Parse("192.168.0.7")
            }
            catch (SocketException)
            {
                Debug.Log("서버가 닫혀있습니다. 시도횟수: " + try_count);
            }
        }
        Debug.Log("서버 접속 성공 ");
    }

    static public void Send_Receive_ToServer(string receiveData)
    {
        byte[] buffer_send = Encoding.Default.GetBytes(receiveData);
        MySocket.Send(buffer_send);

        byte[] buffer_rec = new byte[1024];
        MySocket.Receive(buffer_rec);
        ReceiveData(Encoding.Default.GetString(buffer_rec));
    }

    static public void ReceiveData(string receiveData)
    {
        Debug.Log("서버로부터 받음 : " + receiveData);
        string[] token = receiveData.Split(';');
    
        //계정 생성 성공여부 받아오는 부분
        if (token[0] == "NewAccount")
        {
            if (token[1] == "true")
            {
                Singletone_NetWorkManager.singletone_Network.Success_NewAccount();
            }
            else if (token[1] == "false")
            {
                Singletone_NetWorkManager.singletone_Network.Failed_NewAccount();
            }
        }

        //로그인 성공여부 받아오는 부분
        else if (token[0] == "Login")
        {
            if (token[1] == "true")
            {
                // 로그인성공 및 데이터토큰 저장
                Singletone_NetWorkManager.singletone_Network.Success_Login(token[2]);
               
                //아이디에 맞는 데이터 단순 로드
                Debug.Log("데이터로드 : " + token[3]);
                Singletone_NetWorkManager.singletone_Network.Receive_UserData(token[3]);
            }
            else if (token[1] == "false")
            {
                Singletone_NetWorkManager.singletone_Network.Failed_Login();
            }
        }
        else if (token[0] == "Data")
        {            
            Singletone_NetWorkManager.singletone_Network.Receive_UserData(token[1]);
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