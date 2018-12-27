using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wnd_Cmd_Upgrade : EventWnd_Root
{
    public override void Click_LevelUp() 
    {
        Debug.Log(cmdLevel);
        wndManager.MoveSliderBar(0);
        wndManager.AnimaArrow(0);
        StartCoroutine(timer(cmdLevel));        
    }
    public override void Click_Behaviour()
    {
        string tmp = Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.CmdLevel);
        int increaseTotalRate = int.Parse(tmp) * 10;
        Singletone_PlayerManager.singletone_Player.PlayerInformaion.SetUpdateData(UI_Player_Information.Total, increaseTotalRate.ToString(), 0, 0);

        base.CheckAllOfUpdate();
    }
    public override void LvUp_Effect()
    {
        //
    }
    IEnumerator timer(float time)
    {
        IsDoingUp = true;
        for (int i=0; i< (int)time;i++)
            yield return new WaitForSeconds(1.0f);
        Singletone_PlayerManager.singletone_Player.PlayerInformaion.SetUpdateData(UI_Player_Information.CmdLevel, "1", 0, 0);
        Singletone_PlayerManager.singletone_Player.ShowGlobalAnimation(UI_AnimationName.CmdUp);
        wndManager.Effect_LevelUp(UI_Player_Information.CmdLevel);
        
        IsDoingUp = false;
        Click_Back(0);
        base.CheckAllOfUpdate();
    }
}
