//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_GambleCost : ITableOperate
{ private const string TAB_FILE_DATA = "gamble_cost.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_ONE_COST,
ID_TEN_COST,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_OneCost;
 public int OneCost { get{ return m_OneCost;}}

private int m_TenCost;
 public int TenCost { get{ return m_TenCost;}}

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
 Tab_GambleCost _values = new Tab_GambleCost();
 _values.m_OneCost =  Convert.ToInt32(valuesList[(int)_ID.ID_ONE_COST] as string);
_values.m_TenCost =  Convert.ToInt32(valuesList[(int)_ID.ID_TEN_COST] as string);

 _hash.Add(nKey,_values); }


}
}

