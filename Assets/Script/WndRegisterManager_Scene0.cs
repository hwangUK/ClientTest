using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WndRegisterManager_Scene0 : MonoBehaviour
{
    public List<GameObject> WndList;

    GameObject expt_wnd;
    int currentWnd = 999;
    private void Start()
    {
        expt_wnd = Instantiate(WndList[0]);
        expt_wnd.SetActive(false);
    }

    public void Open(int index)
    {
        expt_wnd.SetActive(true);
        currentWnd = index;
    }

    public void Open(int index, string msg)
    {
        expt_wnd.SetActive(true);
        expt_wnd.transform.GetChild(2).GetComponent<Text>().text = msg;
        currentWnd = index;
    }
    public void Close()
    {
        //currentWnd
        expt_wnd.SetActive(false);
    }
}
