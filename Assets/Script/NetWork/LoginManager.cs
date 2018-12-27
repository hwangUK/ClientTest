using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] GameObject Login_Canvas;
    [SerializeField] GameObject NewAccount_Canvas;
    [SerializeField] GameObject Loading_Canvas;
    [Space(30)]
    [SerializeField] InputField NewAccount_ID;
    [SerializeField] InputField NewAccount_PW;
    [SerializeField] InputField NewAccount_PW_RE;
    [Space(30)]  
    [SerializeField] InputField Login_ID;
    [SerializeField] InputField Login_PW;
    //[Space(30)]
    //[SerializeField] InputField Update_Information; 

    #region 서버로 정보를 보내는 함수
    public void Account_MakeNew()
    {       
        if (NewAccount_PW.text != NewAccount_PW_RE.text)
        {
            Singletone_PlayerManager.singletone_Player.OpenWnd(0,0, "비밀번호 확인이 일치하지 않습니다");
        }

        else if (NewAccount_PW.text == NewAccount_PW_RE.text)
        {
            if (NewAccount_PW.text[0] == '0')
            {
                Singletone_PlayerManager.singletone_Player.OpenWnd(0,0, "0으로 시작하는 비밀번호는 사용 할 수 없습니다");
                NewAccount_PW.text = "";
                NewAccount_PW_RE.text = "";
                return;
            }
            Singletone_NetWorkManager.singletone_Network.Send_Account_Create_New(NewAccount_ID.text, NewAccount_PW.text);
            NewAccount_ID.text = "";
            NewAccount_PW.text = "";
            NewAccount_PW_RE.text = "";
        }
        
        NewAccount_PW.text = "";
        NewAccount_PW_RE.text = "";
    }
    public void Account_Login()
    {
        //exception
        Singletone_NetWorkManager.singletone_Network.Send_Account_Login(Login_ID.text, Login_PW.text);
        
        Login_ID.text = "";
        Login_PW.text = "";
    }
    //public void Update_UserData()
    //{
    //    Singletone_NetWorkManager.Singletone_Information.Send_Update_UserData(Update_Information.text);
    //}
    #endregion

    public void Click_Login(bool onOff)
    {
        if(onOff)
            Login_Canvas.SetActive(true);
        else
            Login_Canvas.SetActive(false);
    }

    public void Click_NewAccount(bool onOff)
    {
        if (onOff)
            NewAccount_Canvas.SetActive(true);
        else
            NewAccount_Canvas.SetActive(false);
    }

}
