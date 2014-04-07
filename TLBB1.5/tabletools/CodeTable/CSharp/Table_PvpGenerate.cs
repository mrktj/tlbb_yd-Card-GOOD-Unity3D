//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_PvpGenerate : ITableOperate
{ private const string TAB_FILE_DATA = "pvp_generate.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_RANK_MIN,
ID_RANK_MAX,
ID_X_MIN,
ID_X_MAX,
ID_Y_MIN,
ID_Y_MAX,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_RankMax;
 public int RankMax { get{ return m_RankMax;}}

private int m_RankMin;
 public int RankMin { get{ return m_RankMin;}}

private int m_XMax;
 public int XMax { get{ return m_XMax;}}

private int m_XMin;
 public int XMin { get{ return m_XMin;}}

private int m_YMax;
 public int YMax { get{ return m_YMax;}}

private int m_YMin;
 public int YMin { get{ return m_YMin;}}

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
 Tab_PvpGenerate _values = new Tab_PvpGenerate();
 _values.m_RankMax =  Convert.ToInt32(valuesList[(int)_ID.ID_RANK_MAX] as string);
_values.m_RankMin =  Convert.ToInt32(valuesList[(int)_ID.ID_RANK_MIN] as string);
_values.m_XMax =  Convert.ToInt32(valuesList[(int)_ID.ID_X_MAX] as string);
_values.m_XMin =  Convert.ToInt32(valuesList[(int)_ID.ID_X_MIN] as string);
_values.m_YMax =  Convert.ToInt32(valuesList[(int)_ID.ID_Y_MAX] as string);
_values.m_YMin =  Convert.ToInt32(valuesList[(int)_ID.ID_Y_MIN] as string);

 _hash.Add(nKey,_values); }


}
}

