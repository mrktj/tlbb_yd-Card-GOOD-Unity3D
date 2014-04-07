//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_PvpnewReward : ITableOperate
{ private const string TAB_FILE_DATA = "pvpnew_reward.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_RANK_MAX,
ID_RANK_MIN,
ID_REWARD_1,
ID_REWARD_1_NUM,
ID_REWARD_2,
ID_REWARD_2_NUM,
ID_REWARD_3,
ID_REWARD_3_NUM,
ID_REWARD_4,
ID_REWARD_4_NUM,
ID_REWARD_5,
ID_REWARD_5_NUM,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_RankMax;
 public int RankMax { get{ return m_RankMax;}}

private int m_RankMin;
 public int RankMin { get{ return m_RankMin;}}

private int[] m_Reward = new int[5];
 public int GetRewardbyIndex(int idx) {
 if(idx>=0 && idx<5) return m_Reward[idx];
 return -1;
 }

private int m_Reward1Num;
 public int Reward1Num { get{ return m_Reward1Num;}}

private int m_Reward2Num;
 public int Reward2Num { get{ return m_Reward2Num;}}

private int m_Reward3Num;
 public int Reward3Num { get{ return m_Reward3Num;}}

private int m_Reward4Num;
 public int Reward4Num { get{ return m_Reward4Num;}}

private int m_Reward5Num;
 public int Reward5Num { get{ return m_Reward5Num;}}

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
 Tab_PvpnewReward _values = new Tab_PvpnewReward();
 _values.m_RankMax =  Convert.ToInt32(valuesList[(int)_ID.ID_RANK_MAX] as string);
_values.m_RankMin =  Convert.ToInt32(valuesList[(int)_ID.ID_RANK_MIN] as string);
_values.m_Reward [ 0 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_1] as string);
_values.m_Reward [ 1 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_2] as string);
_values.m_Reward [ 2 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_3] as string);
_values.m_Reward [ 3 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_4] as string);
_values.m_Reward [ 4 ] =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_5] as string);
_values.m_Reward1Num =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_1_NUM] as string);
_values.m_Reward2Num =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_2_NUM] as string);
_values.m_Reward3Num =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_3_NUM] as string);
_values.m_Reward4Num =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_4_NUM] as string);
_values.m_Reward5Num =  Convert.ToInt32(valuesList[(int)_ID.ID_REWARD_5_NUM] as string);

 _hash.Add(nKey,_values); }


}
}

