using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Singletone_PlayerManager : MonoBehaviour
{
    static public Singletone_PlayerManager singletone_Player = null;
    internal PlayerInformation PlayerInformaion = new PlayerInformation();

    [Header("Register Scene Window handler")][Space(5)]
    private WndRegisterManager_Scene0 handler_wndManager_0;
    private WndRegisterManager_Scene1 handler_wndManager_1;
    [HideInInspector] private UI_AnmationContainer global_Animations;
    private int nowScene_Index = 0;
    public int NowScene_Index   { get => nowScene_Index; set => nowScene_Index = value; }
        
    private void Awake()
    {
        if (singletone_Player == null)
        {
            singletone_Player = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        InitHandlerOnTheScene();
        PlayerInformaion.Init();
        global_Animations = transform.GetChild(0).GetComponent<UI_AnmationContainer>();
    }    

    public void InitPlayerInformation(string s)
    {
        PlayerInformaion.SetUpdateAll(s);
        Debug.Log("로컬에 저장된 정보: " + PlayerInformaion.GetDataStream());
    }
    public void InitHandlerOnTheScene()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                handler_wndManager_0 = GameObject.FindGameObjectWithTag("WndManager_S0").GetComponent<WndRegisterManager_Scene0>();
                break;
            case 1:
                handler_wndManager_1 = GameObject.FindGameObjectWithTag("WndManager_S1").GetComponent<WndRegisterManager_Scene1>();
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
                    handler_wndManager_0.Open(wndIndex, msg);
                break;
            case 1:
                handler_wndManager_1.OpenWindow(wndIndex);
                break;
        }        
    }
    public void CloseWnd()
    {
        switch (sceneIdx)
        {
            case 0:
                handler_wndManager_0.Close();
                break;
            case 1:
                handler_wndManager_1.CloseWindow();
                break;

        }        
    }

    public void ShowGlobalAnimation(UI_AnimationName aniName)
    {
        global_Animations.Show(aniName);
    }
    #endregion
}
