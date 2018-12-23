using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] GameObject Login_Canvas;
    [SerializeField] GameObject NewAccount_Canvas;
    [SerializeField] GameObject Loading_Canvas;


    [SerializeField] InputField NewAccount_ID;
    [SerializeField] InputField NewAccount_PW;
    [SerializeField] InputField Update_Information;

    [SerializeField] InputField Login_ID;
    [SerializeField] InputField Login_PW;
    // Start is called before the first frame update


    public void Account_MakeNew()
    {
        Singletone_Manager.Singletone_Information.Account_Create_New(NewAccount_ID.text, NewAccount_PW.text);
        NewAccount_ID.text = "";
        NewAccount_PW.text = "";
    }
    public void Account_Login()
    {
        Singletone_Manager.Singletone_Information.Account_Login(Login_ID.text, Login_PW.text);
        
        Login_ID.text = "";
        Login_PW.text = "";
    }
    public void Update_UserData()
    {
        Singletone_Manager.Singletone_Information.Update_UserData(Update_Information.text);
    }
}
