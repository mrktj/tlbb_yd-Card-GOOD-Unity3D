//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Yunyinghuodong : ITableOperate
{ private const string TAB_FILE_DATA = "yunyinghuodong.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_DECRIPTION,
ID_TRIGGER_TYPE,
ID_TRIGGER_VALUE,
ID_END_TYPE,
ID_END_VALUE,
ID_REWARD_TYPE,
ID_REWARD_VALUE,
ID_REWARD_NUM,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private string m_Decription;
 public string Decription { get{ return m_Decription;}}

private int m_EndType;
 public int EndType { get{ return m_EndType;}}

private string m_EndValue;
 public string EndValue { get{ return m_EndValue;}}

private string m_RewardNum;
 public string RewardNum { get{ return m_RewardNum;}}

private string m_RewardType;
 public string RewardType { get{ return m_RewardType;}}

private string m_RewardValue;
 public string RewardValue { get{ return m_RewardValue;}}

private int m_TriggerType;
 public int TriggerType { get{ return m_TriggerType;}}

private int m_TriggerValue;
 public int TriggerValue { get{ return m_TriggerValue;}}

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
 Tab_Yunyinghuodong _values = new Tab_Yunyinghuodong();
 _values.m_Decription =  valuesList[(int)_ID.ID_DECRIPTION] as string;
_values.m_EndType =  Convert.ToInt32(valuesList[(int)_ID.ID_END_TYPE] as string);
_values.m_EndValue =  valuesList[(int)_ID.ID_END_VALUE] as string;
_values.m_RewardNum =  valuesList[(int)_ID.ID_REWARD_NUM] as string;
_values.m_RewardType =  valuesList[(int)_ID.ID_REWARD_TYPE] as string;
_values.m_RewardValue =  valuesList[(int)_ID.ID_REWARD_VALUE] as string;
_values.m_TriggerType =  Convert.ToInt32(valuesList[(int)_ID.ID_TRIGGER_TYPE] as string);
_values.m_TriggerValue =  Convert.ToInt32(valuesList[(int)_ID.ID_TRIGGER_VALUE] as string);

 _hash.Add(nKey,_values); }


}
}

