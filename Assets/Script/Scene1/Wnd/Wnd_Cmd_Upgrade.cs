using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wnd_Cmd_Upgrade : MonoBehaviour
{
    public void InitData()
    {
        for (int i = 1; i < 4; i++)
            transform.GetChild(i).GetComponent<UI_Text>().InitShowText();
    }
    public void Click_CmdLevelUp()
    {
        Singletone_PlayerManager.Singletone_Information.PlayerInformaion.SetUpdateData(UI_Player_Information.CmdLevel, "1",0,0);
        InitData();
    }
    public void Click_CmdMakeSoldier()
    {
        string tmp = Singletone_PlayerManager.Singletone_Information.PlayerInformaion.GetDataOne(UI_Player_Information.CmdLevel);
        int tmp_Int = int.Parse(tmp) * 10;
        Singletone_PlayerManager.Singletone_Information.PlayerInformaion.SetUpdateData(UI_Player_Information.Total, tmp_Int.ToString(), 0, 0);

        InitData();
    }
    public void Click_Back()
    {
        GameObject.FindGameObjectWithTag("WndManager_S1").GetComponent<WndRegisterManager_Scene1>().Update_MainUI();
        transform.gameObject.SetActive(false);
    }
}
