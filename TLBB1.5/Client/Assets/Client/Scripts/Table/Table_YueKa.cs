//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_YueKa : ITableOperate
{ private const string TAB_FILE_DATA = "YueKa.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_FLAG,
ID_STARTDATE,
ID_DAYS,
ID_REWARD_DAYS,
ID_RMB,
ID_DROP_TYPE_1,
ID_DROP_ID_1,
ID_DROP_NUM_1,
ID_DROP_TYPE_2,
ID_DROP_ID_2,
ID_DROP_NUM_2,
ID_DROP_TYPE_3,
ID_DROP_ID_3,
ID_DROP_NUM_3,
ID_DROP_TYPE_4,
ID_DROP_ID_4,
ID_DROP_NUM_4,
ID_DROP_TYPE_5,
ID_DROP_ID_5,
ID_DROP_NUM_5,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Days;
 public int Days { get{ return m_Days;}}

private int[] m_DropID = new int[5];
 public int GetDropIDbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_DropID[idx];
 return -1;
 }

private int[] m_DropNum = new int[5];
 public int GetDropNumbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_DropNum[idx];
 return -1;
 }

private int[] m_DropType = new int[5];
 public int GetDropTypebyIndex(int idx) {
 if(idx>=0 && idx<5) return m_DropType[idx];
 return -1;
 }

private int m_Flag;
 public int Flag { get{ return m_Flag;}}

private int m_RewardDays;
 public int RewardDays { get{ return m_RewardDays;}}

private int m_Rmb;
 public int Rmb { get{ return m_Rmb;}}

private int m_Startdate;
 public int Startdate { get{ return m_Startdate;}}

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
 Tab_YueKa _values = new Tab_YueKa();
 _values.m_Days =  Convert.ToInt32(valuesList[(int)_ID.ID_DAYS] as string);
_values.m_DropID [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_ID_1] as string);
_values.m_DropID [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_ID_2] as string);
_values.m_DropID [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_ID_3] as string);
_values.m_DropID [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_ID_4] as string);
_values.m_DropID [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_ID_5] as string);
_values.m_DropNum [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_NUM_1] as string);
_values.m_DropNum [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_NUM_2] as string);
_values.m_DropNum [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_NUM_3] as string);
_values.m_DropNum [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_NUM_4] as string);
_values.m_DropNum [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_NUM_5] as string);
_values.m_DropType [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE_1] as string);
_values.m_DropType [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE_2] as string);
_values.m_DropType [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE_3] as string);
_values.m_DropType [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE_4] as string);
_values.m_DropType [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_DROP_TYPE_5] as string);
_values.m_Flag =  Convert.ToInt32(valuesList[(int)_ID.ID_FLAG] as string);
_values.m_RewardDays =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_DAYS] as string);
_values.m_Rmb =  Convert.ToInt32(valuesList[(int)_ID.ID_RMB] as string);
_values.m_Startdate =  Convert.ToInt32(valuesList[(int)_ID.ID_STARTDATE] as string);

 _hash.Add(nKey,_values); }


}
}

