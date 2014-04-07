//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Activities : ITableOperate
{ private const string TAB_FILE_DATA = "activities.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_FLAG,
ID_STARTDATE,
ID_ENDDATE,
ID_NAME,
ID_CONTENT,
ID_ACTIVITY_TYPE,
ID_ACTIVITY_NUM,
ID_REWARD_TYPE,
ID_REWARD_ID,
ID_REWARD_NUM,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_ActivityNum;
 public int ActivityNum { get{ return m_ActivityNum;}}

private int m_ActivityType;
 public int ActivityType { get{ return m_ActivityType;}}

private string m_Content;
 public string Content { get{ return m_Content;}}

private string m_Enddate;
 public string Enddate { get{ return m_Enddate;}}

private int m_Flag;
 public int Flag { get{ return m_Flag;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_RewardId;
 public int RewardId { get{ return m_RewardId;}}

private int m_RewardNum;
 public int RewardNum { get{ return m_RewardNum;}}

private int m_RewardType;
 public int RewardType { get{ return m_RewardType;}}

private string m_Startdate;
 public string Startdate { get{ return m_Startdate;}}

public bool LoadTable(Hashtable _tab)
 {
 if(!TableManager.ReaderPList(GetInstanceFile(),SerializableTable,_tab))
 {
 throw TableException.ErrorReader("Load File{0} Fail!!!",GetInstanceFile());
 }
 return true;
 }
 public void SerializableTable(ArrayList valuesList,string skey,Hashtable _hash)
 {
 if (string.IsNullOrEmpty(skey))
 {
 throw TableException.ErrorReader("Read File{0} as key is Empty Fail!!!", GetInstanceFile());
 }

 if ((int)_ID.MAX_RECORD!=valuesList.Count)
 {
 throw TableException.ErrorReader("Load {0} error as CodeSize:{1} not Equal DataSize:{2}", GetInstanceFile(),_ID.MAX_RECORD,valuesList.Count);
 }
 Int32 nKey = Convert.ToInt32(skey);
 Tab_Activities _values = new Tab_Activities();
 _values.m_ActivityNum =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIVITY_NUM] as string);
_values.m_ActivityType =  Convert.ToInt32(valuesList[(int)_ID.ID_ACTIVITY_TYPE] as string);
_values.m_Content =  valuesList[(int)_ID.ID_CONTENT] as string;
_values.m_Enddate =  valuesList[(int)_ID.ID_ENDDATE] as string;
_values.m_Flag =  Convert.ToInt32(valuesList[(int)_ID.ID_FLAG] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_RewardId =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_ID] as string);
_values.m_RewardNum =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_NUM] as string);
_values.m_RewardType =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_TYPE] as string);
_values.m_Startdate =  valuesList[(int)_ID.ID_STARTDATE] as string;

 _hash.Add(nKey,_values); }


}
}

