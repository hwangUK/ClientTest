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

    public int CmdBuildingLevel { get => cmdBuildingLevel; set => cmdBuildingLevel = value; }
    public int CmdBuilding_AmountOfCanBeMaking { get => cmdBuilding_AmountOfCanBeMaking; set => cmdBuilding_AmountOfCanBeMaking = value; }

    public List<GameObject> WndList;
    private List<GameObject> stack_Wnd = new List<GameObject>();

    public GameObject UI_Main_TextList;
    // Start is called before the first frame update
    void Awake()
    {
        for(int i=0; i< WndList.Count; i++)
        {
            GameObject newWnd = Instantiate(WndList[i]);
            newWnd.SetActive(false);
            stack_Wnd.Add(newWnd);
        }       
    }

    private int currentWnd = 999;
    public void Open(int index)
    {
        stack_Wnd[index].gameObject.SetActive(true);
        switch (index)
        {
            case 0:
                stack_Wnd[0].transform.GetComponent<Wnd_Cmd_Upgrade>().InitData();
                currentWnd = 0;
                break;
        }            
    } 
    public void Close()
    {
        stack_Wnd[currentWnd].SetActive(false);
    }

    public void Update_MainUI()
    {
        for(int i=0; i < UI_Main_TextList.transform.childCount; i++)
        {
            UI_Main_TextList.transform.GetChild(i).GetComponent<UI_Text>().InitShowText();
        }
    }
}
