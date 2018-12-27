public enum UI_Player_Information{
    Default,
    ID,
    Level,
    CmdLevel, FireWallLevel_0,
    Hero_Have, Hero_Level,
    Total
}
public enum UI_Calculated_Information{
    Default,
    CmdCanDoingAmount,FireWallDoing,
}
public enum UI_TypeOfBuilding {
    Default,
    Cmd, Building_1, Building_2, Building_3, Building_4
}
public enum UI_TypeOfWnd {
    Default,
    Wnd_Cmd, Building_1, Building_2, Building_3, Building_4
}
public enum MakeAmount
{
    Default,
    Cmd = 10, Firewall = 7
}
public enum UI_AnimationName
{
    Default,
    LevelUp, CmdUp, FWallUp
}
class PlayerInformation
{
    private string m_DataStream;
    private string m_ID;
    private string m_Level;
    private string m_MyCmdLevel;
    private string m_HeroHaveingState;
    private string m_HeroLevelState;
    private string m_Total;
    private string[] m_MySubBuildingList = new string[5];

    public void Init()
    {
        m_DataStream ="0";
        m_Level = "0";
        m_MyCmdLevel = "0";
        m_HeroHaveingState = "0000000000";
        m_HeroLevelState = "0000000000";
        m_Total = "0";
        for (int i = 0; i < m_MySubBuildingList.Length; i++)
        {
            m_MySubBuildingList[i] = "0";
        }
    }
    public void Init_SetID(string id)
    {
        m_ID = id;
    }
    public void SetUpdateAll(string inputStream)
    {
        m_DataStream = inputStream;
        string[] str = inputStream.Split('/');
        m_Total = str[0];
        m_Level = str[1];
        m_MyCmdLevel = str[2];
        m_HeroHaveingState = str[3];
        m_HeroLevelState = str[4];
        for (int i=0; i< m_MySubBuildingList.Length;i++)
        {
            m_MySubBuildingList[i] = str[i + 5];
        }        
    }
    
    public void SetUpdateData(UI_Player_Information type, string updateIncreaseValue, int heroIndex, int heroUpAmount)
    {
        switch (type)
        {
            case UI_Player_Information.Total:
                m_Total = (int.Parse(m_Total) + int.Parse(updateIncreaseValue)).ToString();
                break;
            case UI_Player_Information.Level:
                m_Level = (int.Parse(m_Level) + int.Parse(updateIncreaseValue)).ToString();
                break;
            case UI_Player_Information.CmdLevel:
                m_MyCmdLevel = (int.Parse(m_MyCmdLevel) + int.Parse(updateIncreaseValue)).ToString();
                break;
            case UI_Player_Information.FireWallLevel_0:
                m_MySubBuildingList[0] = (int.Parse(m_MySubBuildingList[0]) + int.Parse(updateIncreaseValue)).ToString();
                break;
            case UI_Player_Information.Hero_Have:
                //비트 마스크
                m_HeroHaveingState = updateIncreaseValue;
                break;
            case UI_Player_Information.Hero_Level:
                m_HeroLevelState = updateIncreaseValue;
                break;

        }
        m_DataStream = m_Total + "/" + m_Level + "/" + m_MyCmdLevel + "/" + m_HeroHaveingState + "/" + m_HeroLevelState + "/"; 
        for (int i = 0; i < m_MySubBuildingList.Length; i++)
        {
            m_DataStream += (m_MySubBuildingList[i] + "/");
        }
        Singletone_NetWorkManager.singletone_Network.Send_Update_UserData(m_DataStream);
    }
    public string GetDataStream()
    {
        return m_DataStream;
    }
    public string GetDataOne(UI_Player_Information type)
    {
        switch (type)
        {
            case UI_Player_Information.ID:
                return m_ID;
            case UI_Player_Information.Total:
                return m_Total;
            case UI_Player_Information.Level:
                return m_Level;
            case UI_Player_Information.CmdLevel:
                return m_MyCmdLevel;
            case UI_Player_Information.FireWallLevel_0:
                return m_MySubBuildingList[0];
            case UI_Player_Information.Hero_Have:
                return m_HeroHaveingState;
            case UI_Player_Information.Hero_Level:
                return m_HeroLevelState;
        }
        return "Exception";
    }
}