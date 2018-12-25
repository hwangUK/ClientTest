using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
struct PlayerInformation
{
    private string DataStream;
    private string ID;
    private string Level;
    private string MyCmdLevel;
    private string HeroHaveingState;
    private string HeroLevelState;
    private string Total;


    public void SetUpdateAll(string inputStream)
    {
        DataStream = inputStream;
        string[] str = inputStream.Split('/');
        Total = str[0];
        Level = str[1];
        MyCmdLevel = str[2];
        HeroHaveingState = str[3];
        HeroLevelState = str[4];
    }
    public void SetUpdateData(UI_Player_Information type, string updateIncreaseValue, int heroIndex, int heroUpAmount)
    {
        switch (type)
        {
            case UI_Player_Information.ID:
                ID = updateIncreaseValue;
                break;

            //DataStream
            case UI_Player_Information.Total:
                Total = (int.Parse(Total) + int.Parse(updateIncreaseValue)).ToString();
                break;
            case UI_Player_Information.Level:
                Level = (int.Parse(Level) + int.Parse(updateIncreaseValue)).ToString();
                break;
            case UI_Player_Information.CmdLevel:
                MyCmdLevel = (int.Parse(MyCmdLevel) + int.Parse(updateIncreaseValue)).ToString();
                break;
            case UI_Player_Information.Hero_Have:
                //비트 마스크
                HeroHaveingState = updateIncreaseValue;
                break;
            case UI_Player_Information.Hero_Level:
                HeroLevelState = updateIncreaseValue;
                break;
            
        }
        DataStream = Total + "/" + Level + "/" + MyCmdLevel + "/" + HeroHaveingState + "/" + HeroLevelState + "/";
        Singletone_NetWorkManager.Singletone_Information.Send_Update_UserData(DataStream);
    }
    public string GetDataStream()
    {
        return DataStream;
    }
    public string GetDataOne(UI_Player_Information type)
    {
        switch (type)
        {
            case UI_Player_Information.ID:
                return ID;
            case UI_Player_Information.Total:
                return Total; 
            case UI_Player_Information.Level:
                return Level;
            case UI_Player_Information.CmdLevel:
                return MyCmdLevel;
            case UI_Player_Information.Hero_Have:
                return HeroHaveingState;
            case UI_Player_Information.Hero_Level:
                return HeroLevelState;
        }
        return "Exception";
    }
}

public class Singletone_PlayerManager : MonoBehaviour
{
    static public Singletone_PlayerManager Singletone_Information = null;
    internal PlayerInformation PlayerInformaion = new PlayerInformation();
    private WndRegisterManager_Scene0 wndManager_0;
    private WndRegisterManager_Scene1 wndManager_1;
    private int nowScene_Index = 0;

    public int NowScene_Index   { get => nowScene_Index; set => nowScene_Index = value; }

    
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
        InitNewSceneInformation();
        
    }    

    public void InitPlayerInformation(string s)
    {
        PlayerInformaion.SetUpdateAll(s);
        Debug.Log("로컬에서 읽어들인 정보: " + PlayerInformaion.GetDataStream());
    }

    public void InitNewSceneInformation()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                wndManager_0 = GameObject.FindGameObjectWithTag("WndManager_S0").GetComponent<WndRegisterManager_Scene0>();
                break;
            case 1:
                wndManager_1 = GameObject.FindGameObjectWithTag("WndManager_S1").GetComponent<WndRegisterManager_Scene1>();
                break;
        }
    }

    #region Wnd
    int sceneIdx = 999;
    public void OpenWnd(int sceneIndex, int wndIndex, string msg)
    {
        sceneIdx = sceneIndex;
        switch (sceneIndex)
        {
            case 0:
                if(wndIndex == 0)
                    wndManager_0.Open(wndIndex, msg);
                break;
            case 1:
                wndManager_1.Open(wndIndex);
                break;
        }        
    }
    public void CloseWnd()
    {
        switch (sceneIdx)
        {
            case 0:
                wndManager_0.Close();
                break;
            case 1:
                wndManager_1.Close();
                break;

        }        
    }
    #endregion
}
