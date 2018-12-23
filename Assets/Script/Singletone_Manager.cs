using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone_Manager : MonoBehaviour
{
    static public Singletone_Manager Singletone_Information = null;
    Socket_Client My_Local_Server;

    #region Awake
    private void Awake()
    {
        if (Singletone_Information == null)
        {
            Singletone_Information = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        My_Local_Server = transform.GetComponent<Socket_Client>();
    }
    #endregion

    public string PlayerID = "";
    public string PlayerPW = "";

    public void Read_ID_PW(string id, string pw)
    {
        PlayerID = id;
        PlayerPW = pw;
    }
    public void Login_Success()
    {
        Debug.Log("로그인성공");
    }
    public void Login_Failed()
    {
        Debug.Log("로그인실패");
    }
    public void Login_Check(string ID, string PW)
    {
        My_Local_Server.SendToServer("Login;"+ ID + ";" + PW+";");
    }
}
