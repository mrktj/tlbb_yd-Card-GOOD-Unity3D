//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_Shopping : ITableOperate
{ private const string TAB_FILE_DATA = "shopping.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_NOTE,
ID_COST,
ID_INCREASENUM,
ID_LIMIT,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_Cost;
 public int Cost { get{ return m_Cost;}}

private int m_IncreaseNum;
 public int IncreaseNum { get{ return m_IncreaseNum;}}

private int m_Limit;
 public int Limit { get{ return m_Limit;}}

private int m_Note;
 public int Note { get{ return m_Note;}}

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
 Tab_Shopping _values = new Tab_Shopping();
 _values.m_Cost =  Convert.ToInt32(valuesList[(int)_ID.ID_COST] as string);
_values.m_IncreaseNum =  Convert.ToInt32(valuesList[(int)_ID.ID_INCREASENUM] as string);
_values.m_Limit =  Convert.ToInt32(valuesList[(int)_ID.ID_LIMIT] as string);
_values.m_Note =  Convert.ToInt32(valuesList[(int)_ID.ID_NOTE] as string);

 _hash.Add(nKey,_values); }


}
}

