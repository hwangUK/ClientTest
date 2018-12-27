using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AnmationContainer : MonoBehaviour
{
    private GameObject gob;

    public void Show(UI_AnimationName type)
    {        
        if (type == UI_AnimationName.LevelUp)
            gob = transform.GetChild(0).GetChild(0).gameObject;
        else if (type == UI_AnimationName.CmdUp)
            gob = transform.GetChild(0).GetChild(1).gameObject;
        else if (type == UI_AnimationName.FWallUp)
            gob = transform.GetChild(0).GetChild(2).gameObject;
        else
            return;
        gob.SetActive(true);
        StartCoroutine(Co_Ani());
    }
    IEnumerator Co_Ani()
    {
        Vector3 saveLocalPos = gob.transform.position;
        for (int i = 0; i < 50; i++)
        {
            gob.transform.Translate(Vector3.up * Time.deltaTime * 25.0f);
            yield return new WaitForFixedUpdate();
        }
        gob.transform.position = saveLocalPos;
        gob.SetActive(false);
    }   
}
