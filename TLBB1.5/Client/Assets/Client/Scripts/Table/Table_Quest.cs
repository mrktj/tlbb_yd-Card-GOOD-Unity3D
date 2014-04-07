//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Quest : ITableOperate
{ private const string TAB_FILE_DATA = "quest.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NAME,
ID_IS_LOOP,
ID_QUEST_TYPE,
ID_QUEST_VALUE,
ID_BACK_ID,
ID_REWARD_GOLD,
ID_REWARD_DOLLAR,
ID_REWARD_POWER,
ID_REWARD_CHANCE,
ID_REWARD_ID,
ID_ATTITEM_NUM,
ID_HPITEM_NUM,
ID_REWARD_ONE,
ID_REWARD_TWO,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_AttitemNUM;
 public int AttitemNUM { get{ return m_AttitemNUM;}}

private int m_BackID;
 public int BackID { get{ return m_BackID;}}

private int m_HpitemNUM;
 public int HpitemNUM { get{ return m_HpitemNUM;}}

private int m_IsLoop;
 public int IsLoop { get{ return m_IsLoop;}}

private string m_Name;
 public string Name { get{ return m_Name;}}

private int m_QuestType;
 public int QuestType { get{ return m_QuestType;}}

private int m_QuestValue;
 public int QuestValue { get{ return m_QuestValue;}}

private int m_RewardID;
 public int RewardID { get{ return m_RewardID;}}

private int m_RewardChance;
 public int RewardChance { get{ return m_RewardChance;}}

private int m_RewardDollar;
 public int RewardDollar { get{ return m_RewardDollar;}}

private int m_RewardGold;
 public int RewardGold { get{ return m_RewardGold;}}

private string m_RewardOne;
 public string RewardOne { get{ return m_RewardOne;}}

private int m_RewardPower;
 public int RewardPower { get{ return m_RewardPower;}}

private string m_RewardTwo;
 public string RewardTwo { get{ return m_RewardTwo;}}

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
 Tab_Quest _values = new Tab_Quest();
 _values.m_AttitemNUM =  Convert.ToInt32(valuesList[(int)_ID.ID_ATTITEM_NUM] as string);
_values.m_BackID =  Convert.ToInt32(valuesList[(int)_ID.ID_BACK_ID] as string);
_values.m_HpitemNUM =  Convert.ToInt32(valuesList[(int)_ID.ID_HPITEM_NUM] as string);
_values.m_IsLoop =  Convert.ToInt32(valuesList[(int)_ID.ID_IS_LOOP] as string);
_values.m_Name =  valuesList[(int)_ID.ID_NAME] as string;
_values.m_QuestType =  Convert.ToInt32(valuesList[(int)_ID.ID_QUEST_TYPE] as string);
_values.m_QuestValue =  Convert.ToInt32(valuesList[(int)_ID.ID_QUEST_VALUE] as string);
_values.m_RewardID =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_ID] as string);
_values.m_RewardChance =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_CHANCE] as string);
_values.m_RewardDollar =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_DOLLAR] as string);
_values.m_RewardGold =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_GOLD] as string);
_values.m_RewardOne =  valuesList[(int)_ID.ID_REWARD_ONE] as string;
_values.m_RewardPower =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_POWER] as string);
_values.m_RewardTwo =  valuesList[(int)_ID.ID_REWARD_TWO] as string;

 _hash.Add(nKey,_values); }


}
}

