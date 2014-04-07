//This code create by CodeEngine mrd.cyou.com ,don't modify
using System;
 using System.Collections.Generic;
 using System.Collections;

namespace GCGame.Table{

[Serializable]
 public class Tab_GoldPrice : ITableOperate
{ private const string TAB_FILE_DATA = "GoldPrice.txt";
 public enum _ID
 {
 INVLAID_INDEX=-1,
ID_POWER_PRICE,
ID_GOLD_VALUE,
MAX_RECORD
 }
 public string GetInstanceFile(){return TAB_FILE_DATA; }

private int m_GoldValue;
 public int GoldValue { get{ return m_GoldValue;}}

private int m_PowerPrice;
 public int PowerPrice { get{ return m_PowerPrice;}}

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
 Tab_GoldPrice _values = new Tab_GoldPrice();
 _values.m_GoldValue =  Convert.ToInt32(valuesList[(int)_ID.ID_GOLD_VALUE] as string);
_values.m_PowerPrice =  Convert.ToInt32(valuesList[(int)_ID.ID_POWER_PRICE] as string);

 _hash.Add(nKey,_values); }


}
}

