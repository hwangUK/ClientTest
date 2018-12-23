using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] GameObject Login_Canvas;
    [SerializeField] GameObject NewAccount_Canvas;
    [SerializeField] GameObject Loading_Canvas;

    [SerializeField] InputField ID_Login;
    [SerializeField] InputField PW_Login;
    [SerializeField] InputField ID_New;
    [SerializeField] InputField PW_New;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Set()
    {
        Singletone_Manager.Singletone_Information.Login_Check(ID_Login.text, PW_Login.text);
        ID_Login.text = "";
        PW_Login.text = "";
    }

    void Login()
    {

    }
}
