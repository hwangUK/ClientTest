using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singletone_NetWorkManager : MonoBehaviour
{
    static public Singletone_NetWorkManager singletone_Network = null;   

    private void Awake()
    {
        if (singletone_Network == null)
        {
            singletone_Network = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Local
    public void Success_NewAccount()
    {
        Debug.Log("계정 생성 완료");
        Singletone_PlayerManager.singletone_Player.OpenWnd(0,0, "계정 생성 완료!"); 
    }
    public void Failed_NewAccount()
    {
        Debug.Log("이미 존재하는 아이디입니다.");
        Singletone_PlayerManager.singletone_Player.OpenWnd(0, 0, "이미 존재하는 아이디입니다.");
    }

    public void Success_Login(string id)
    {
        Debug.Log("로그인 성공");        
        Singletone_PlayerManager.singletone_Player.PlayerInformaion.Init_SetID(id);//자신 아이디 저장
        SceneManager.LoadScene(++Singletone_PlayerManager.singletone_Player.NowScene_Index);
    }
    public void Failed_Login()
    {
        Debug.Log("다시 입력해 주세요");
        Singletone_PlayerManager.singletone_Player.OpenWnd(0, 0, "비밀번호 또는 아이디를 확인해 주세요");
    }
    public void Receive_UserData(string receive_data_stream)
    {
        Singletone_PlayerManager.singletone_Player.InitPlayerInformation(receive_data_stream);
    }
    #endregion

    #region Server
    //서버로 보내는 부분
    public void Send_Account_Create_New(string ID, string PW)
    {
        //새로만드는 계정정보 보내기 NewAccount 토큰
        Socket_Client.Send_Receive_ToServer("NewAccount;" + ID + ";" + PW+";");
    }
    public void Send_Account_Login(string ID, string PW)
    {
        //로그인 계정정보 보내기 Login 토큰
        Socket_Client.Send_Receive_ToServer("Login;" + ID + ";" + PW + ";");
    }

    public void Send_Update_UserData(string data_stream)
    {
        //아이디와 함께 변경할 캐릭터 정보 보내기 Data 토큰
        Socket_Client.Send_Receive_ToServer("Data;"+ 
            Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.ID) + ";" + data_stream + ";");
    }
    #endregion
}
