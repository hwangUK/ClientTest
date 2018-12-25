using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text : MonoBehaviour
{
    public UI_Player_Information TypeOfInformation;
    public UI_TypeOfBuilding TypeOfBuilding;
    public UI_Calculated_Information TypeOfCalculateInfor;

    public string CustomSet_Title;
    public string CustomSet_Contents_CustomSet;
    private string Showtext;
    // Start is called before the first frame update
    private void Start()
    {
        InitShowText();
    }
    public void InitShowText()
    {
        if(TypeOfInformation != UI_Player_Information.Default)
        {
            Showtext = Singletone_PlayerManager.Singletone_Information.PlayerInformaion.GetDataOne(TypeOfInformation);
            transform.GetChild(2).GetComponent<Text>().text = TypeOfInformation.ToString();
            transform.GetChild(3).GetComponent<Text>().text = Showtext;
        }
        else if(TypeOfBuilding != UI_TypeOfBuilding.Default)
        {
            //빌딩정보 받아오기
            transform.GetChild(2).GetComponent<Text>().text = "-";
            transform.GetChild(3).GetComponent<Text>().text = "-";
        }
        else if (TypeOfCalculateInfor != UI_Calculated_Information.Default)
        {
            //빌딩정보 받아오기
            switch (TypeOfCalculateInfor)
            {
                case UI_Calculated_Information.CmdCanDoingAmount:
                    Showtext = (int.Parse(Singletone_PlayerManager.Singletone_Information.PlayerInformaion.GetDataOne(UI_Player_Information.CmdLevel)) * 10).ToString();
                    transform.GetChild(2).GetComponent<Text>().text = "생성 가능 인원";
                    transform.GetChild(3).GetComponent<Text>().text = Showtext;
                    break;
            }            
        }
        else
        {
            transform.GetChild(2).GetComponent<Text>().text = CustomSet_Title;
            transform.GetChild(3).GetComponent<Text>().text = CustomSet_Contents_CustomSet;
        }
    }
}
