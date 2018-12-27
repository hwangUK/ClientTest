using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WndRegisterManager_Scene1 : MonoBehaviour
{
    //building
    private int cmdBuildingLevel;
    private int cmdBuilding_AmountOfCanBeMaking;
    private List<GameObject> stack_Wnd = new List<GameObject>();

    public int CmdBuildingLevel { get => cmdBuildingLevel; set => cmdBuildingLevel = value; }
    public int CmdBuilding_AmountOfCanBeMaking { get => cmdBuilding_AmountOfCanBeMaking; set => cmdBuilding_AmountOfCanBeMaking = value; }
    [Header("SlideBar_InWorld")][Space(3)]
    public List<Slider> registerSliderBar = new List<Slider>();
    [Header("SlideBar_InUICanvas")][Space(3)]
    public List<Slider> registerSliderBar_UI = new List<Slider>();
    [Space(3)]
    public GameObject UI_Main_TextList;

    public GameObject obj_CmdModel;
    public GameObject obj_fireWallModel;

    public Text levelBoard_Cmd;
    public Text levelBoard_FireWall;
    public List<Material> matList = new List<Material>();

    public UI_Anim[] AnimArrow;
    public List<Text> textList_time_InCanvas = new List<Text>();
    public List<Text> textList_time_InWorld = new List<Text>(); 
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=2; i< transform.childCount; i++)
        {
            GameObject newWnd = transform.GetChild(i).gameObject;
            stack_Wnd.Add(newWnd);
            newWnd.SetActive(false);
        }
        Update_MainUI();
        Init();
    }

    #region WndOpenClose
    private int currentWnd = -1;
    public void OpenWindow(int index)
    {
        stack_Wnd[index].gameObject.SetActive(true);
        switch (index)
        {
            case 0:
                stack_Wnd[0].transform.GetComponent<Wnd_Cmd_Upgrade>().CheckAllOfUpdate();
                currentWnd ++;
                break;
            case 1:
                stack_Wnd[1].transform.GetComponent<Wnd_FireWall>().CheckAllOfUpdate();
                currentWnd ++;
                break;
        }            
    } 
    public void CloseWindow()
    {
        stack_Wnd[currentWnd].SetActive(false);
        currentWnd--;
    }
    #endregion

    void Init()
    {
        for (int i=0; i< textList_time_InWorld.Capacity; i++)
        {
            textList_time_InCanvas[i].text = "";
            textList_time_InWorld[i].text = "";
        }        
    }

    #region SliderMoveAndEffect
    public void MoveSliderBar(int buildingIndex) //슬라이드바 시간 설정 하는 곳
    {
        //시간설정 레벨 비례
        int time = 1;
        if(buildingIndex == 0)
            time = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.CmdLevel));
        else if (buildingIndex == 1)
            time = int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.FireWallLevel_0));
        if (time == 0) time = 1;
        StartCoroutine(Co_MoveSlider(buildingIndex, time));
    }
    private IEnumerator Co_MoveSlider(int buildingIndex, int time)
    {
        //월드 좌표 UI_ 슬라이더, 시간텍스트 업데이트
        registerSliderBar[buildingIndex].value = registerSliderBar[buildingIndex].minValue;
        registerSliderBar_UI[buildingIndex].value = registerSliderBar_UI[buildingIndex].minValue;
        float timer = (float)time + 1.0f;
        time *= 10;        
        for (int i = 0; i < time; i++)
        {
            float f = 60.0f / (float)time;            
            timer -= 0.1f;
            registerSliderBar[buildingIndex].value += f;
            registerSliderBar_UI[buildingIndex].value += f;
            textList_time_InWorld[buildingIndex].text = ((int)timer).ToString();
            textList_time_InCanvas[buildingIndex].text = ((int)timer).ToString();
            yield return new WaitForSeconds(0.1f);
        }
        textList_time_InWorld[buildingIndex].text = "";
        textList_time_InCanvas[buildingIndex].text = "";
        registerSliderBar[buildingIndex].value = registerSliderBar[buildingIndex].minValue;
        registerSliderBar_UI[buildingIndex].value = registerSliderBar_UI[buildingIndex].minValue;
    }
    public void Effect_LevelUp(UI_Player_Information type)
    {
        if (int.Parse(Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(type)) > 12)
            return;
        StartCoroutine(Co_MoveObject(type));
    }
    private IEnumerator Co_MoveObject(UI_Player_Information bulidingIndex)
    {
        GameObject obj;

        if (bulidingIndex == UI_Player_Information.CmdLevel)
            obj = obj_CmdModel;
        else if (bulidingIndex == UI_Player_Information.FireWallLevel_0)
            obj = obj_fireWallModel;
        else yield break;
        for (int i = 0; i < 20; i++)
        {
            obj.transform.Translate(Vector3.up * Time.deltaTime * 10.0f);
            obj.transform.Rotate(Vector3.up * Time.deltaTime * 10.0f);
            yield return new WaitForSeconds(0.15f);
        }
    }
    public void AnimaArrow(int wndindex)
    {
        if (wndindex == 0)
        {
            AnimArrow[0].StartAnim();
            AnimArrow[1].StartAnim();
        }
        if (wndindex == 1)
        {
            AnimArrow[2].StartAnim();
            AnimArrow[3].StartAnim();
        }
    }
    #endregion

    //모든 배경 UI 텍스트 업데이트 
    public void Update_MainUI()
    {
        for(int i=0; i < UI_Main_TextList.transform.childCount; i++)
        {
            UI_Main_TextList.transform.GetChild(i).GetComponent<UI_DynamicText>().InitShowText();
        }
        levelBoard_Cmd.text = Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.CmdLevel);
        levelBoard_FireWall.text = Singletone_PlayerManager.singletone_Player.PlayerInformaion.GetDataOne(UI_Player_Information.FireWallLevel_0);
    }

    
}
