using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone_Manager : MonoBehaviour
{
    static public Singletone_Manager Singletone_Information = null;
    string m_ID = "";

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

    }
    #endregion

    public void Success_NewAccount()
    {
        Debug.Log("계정 생성 완료");
    }
    public void Failed_NewAccount()
    {
        Debug.Log("이미 존재하는 아이디입니다.");
    }

    public void Success_Login(string id)
    {
        Debug.Log("로그인 성공");
        //자신 아이디 저장
        m_ID = id;
    }
    public void Failed_Login()
    {
        Debug.Log("다시 입력해 주세요");
    }
    public void Load_UserData(string receive_data_stream)
    {
        Debug.Log("서버에서 유저 정보 로드: " + receive_data_stream);
    }


    public void Account_Create_New(string ID, string PW)
    {
        Socket_Client.Send_Receive_ToServer("NewAccount;" + ID + ";" + PW+";");
    }
    public void Account_Login(string ID, string PW)
    {
        Socket_Client.Send_Receive_ToServer("Login;" + ID + ";" + PW + ";");
    }

    

    public void Update_UserData(string data_stream)
    {
        Socket_Client.Send_Receive_ToServer("Data;"+ m_ID+";" + data_stream + ";");
    }
}
