//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_PvpScore : ITableOperate
{ private const string TAB_FILE_DATA = "pvp_score.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_RANK_MIN,
ID_RANK_MAX,
ID_SCORE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_RankMax;
 public int RankMax { get{ return m_RankMax;}}

private int m_RankMin;
 public int RankMin { get{ return m_RankMin;}}

private int m_Score;
 public int Score { get{ return m_Score;}}

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
 Tab_PvpScore _values = new Tab_PvpScore();
 _values.m_RankMax =  Convert.ToInt32(valuesList[(int)_ID.ID_RANK_MAX] as string);
_values.m_RankMin =  Convert.ToInt32(valuesList[(int)_ID.ID_RANK_MIN] as string);
_values.m_Score =  Convert.ToInt32(valuesList[(int)_ID.ID_SCORE] as string);

 _hash.Add(nKey,_values); }


}
}

