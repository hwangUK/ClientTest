using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wnd_FireWall : EventWnd_Root
{
    public GameObject textBoxList_fwall;
    
    public override void Click_LevelUp()
    {
        Debug.Log(fWallLevel);
        wndManager.MoveSliderBar(1);
        wndManager.AnimaArrow(1);
        StartCoroutine(timer(fWallLevel));
        
        base.CheckAllOfUpdate();        
    }

    public override void Click_Behaviour()
    {
        string tmp = Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.FireWallLevel_0);
        int tmp_Int = int.Parse(tmp) * (int)MakeAmount.Firewall;
        Singletone_PlayerManager.singletone_Player.PlayerInformaion.SetUpdateData(UI_Player_Information.Total, tmp_Int.ToString(), 0, 0);

        base.CheckAllOfUpdate();
    }
    public override void LvUp_Effect()
    {

    }
    IEnumerator timer(float time)
    {
        IsDoingUp = true;
        for (int i = 0; i < (float)time; i++)
            yield return new WaitForSeconds(1.0f);
        Singletone_PlayerManager.singletone_Player.PlayerInformaion.SetUpdateData(UI_Player_Information.FireWallLevel_0, "1", 0, 0);
        Singletone_PlayerManager.singletone_Player.ShowGlobalAnimation(UI_AnimationName.FWallUp);
        wndManager.Effect_LevelUp(UI_Player_Information.FireWallLevel_0);
        
        IsDoingUp = false;
        Click_Back(1);
        base.CheckAllOfUpdate();
    }
}
