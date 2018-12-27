using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventWnd_Root : MonoBehaviour
{
    public bool IsDoingUp = false;
    protected WndRegisterManager_Scene1 wndManager;
    protected int cmdLevel;
    protected int fWallLevel;
    
    private void Awake()
    {
        cmdLevel = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.CmdLevel));
        fWallLevel = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.FireWallLevel_0));
        wndManager = GameObject.FindGameObjectWithTag("WndManager_S1").GetComponent<WndRegisterManager_Scene1>();
    }
    private void Start()
    {
        CheckAllOfUpdate();
    }

    public void CheckAllOfUpdate()
    {
        cmdLevel = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.CmdLevel));
        fWallLevel = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.FireWallLevel_0));

        int nowPlayerLv = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.Level));
        int nowTotalStats = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.Total));
        int tmp;
        if ((tmp = nowTotalStats / 100) > nowPlayerLv)//최종전투력을 통해 레벨업 시키기
        {
            Singletone_PlayerManager.singletone_Player.PlayerInformaion.SetUpdateData(UI_Player_Information.Level, (tmp - nowPlayerLv).ToString(), 0, 0);
            Singletone_PlayerManager.singletone_Player.ShowGlobalAnimation(UI_AnimationName.LevelUp);
            LvUp_Effect();
        }
        //해당 윈도우 창 업데이트
        for (int i = 1; i < 4; i++)
            transform.GetChild(i).GetComponent<UI_DynamicText>().InitShowText();

        //배경UI 업데이트
        switch (Singletone_PlayerManager.singletone_Player.NowScene_Index)
        {
            case 0:
                break;
            case 1:
                GameObject.FindGameObjectWithTag("WndManager_S1").GetComponent<WndRegisterManager_Scene1>().Update_MainUI();
                break;
            case 2:
                break;
        }
    }
    //재정의 함수
    public abstract void Click_LevelUp();
    public abstract void Click_Behaviour();
    public abstract void LvUp_Effect();
    public void Click_Back(int WndIndex)
    {
        if(IsDoingUp)
            transform.gameObject.GetComponent<Canvas>().enabled =false;
        else
        {
            transform.gameObject.GetComponent<Canvas>().enabled = true;
            transform.gameObject.SetActive(false);
        }
            
    }
}
